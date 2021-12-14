using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Hx.Sdk.Attributes;
using Hx.Sdk.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace HxCore.Aops
{
    /// <summary>
    /// 缓存拦截器
    /// </summary>
    internal class BlogMemoryCacheAop : CacheAOPbase
    {
        private readonly IMemoryCache _cache;
        public BlogMemoryCacheAop(IMemoryCache cache)
        {
            _cache = cache;
        }
        public override void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            if (method.ReturnType == typeof(void) || method.ReturnType == typeof(Task))
            {
                invocation.Proceed();
                return;
            }
            //对当前方法的特性验证
            var cachingAttribute = method.GetCustomAttributes(typeof(CacheDataAttribute), true).FirstOrDefault() as CacheDataAttribute;
            if (cachingAttribute == null)
            {
                invocation.Proceed();//直接执行被拦截方法
            }
            else
            {
                //获取自定义缓存键
                var cacheKey = CustomCacheKey(invocation);
                //注意是 string 类型，方法GetValue
                var cacheValue = _cache.Get<string>(cacheKey);
                if (cacheValue != null)
                {
                    //将当前获取到的缓存值，赋值给当前执行方法
                    Type returnType;
                    var isTask = typeof(Task).IsAssignableFrom(method.ReturnType);
                    if (isTask)
                    {
                        returnType = method.ReturnType.GenericTypeArguments.FirstOrDefault();
                    }
                    else
                    {
                        returnType = method.ReturnType;
                    }
                    dynamic _result = JsonConvert.DeserializeObject(cacheValue, returnType);
                    invocation.ReturnValue = isTask ? Task.FromResult(_result) : _result;
                    return;
                }
                //去执行当前的方法
                invocation.Proceed();

                //存入缓存
                if (!string.IsNullOrWhiteSpace(cacheKey))
                {
                    object response;
                    //Type type = invocation.ReturnValue?.GetType();
                    var type = invocation.Method.ReturnType;
                    if (typeof(Task).IsAssignableFrom(type))
                    {
                        var resultProperty = type.GetProperty("Result");
                        response = resultProperty.GetValue(invocation.ReturnValue);
                    }
                    else
                    {
                        response = invocation.ReturnValue;
                    }
                    if (response == null) response = string.Empty;
                    _cache.Set(cacheKey, response, TimeSpan.FromMinutes(cachingAttribute.AbsoluteExpiration));
                }
            }
        }
    }
}
