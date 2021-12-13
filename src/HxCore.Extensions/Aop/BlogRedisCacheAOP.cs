using Castle.DynamicProxy;
using Hx.Sdk.Attributes;
using Hx.Sdk.Cache;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Hx.Sdk.DependencyInjection;

namespace HxCore.Extensions.Aop
{
    /// <summary>
    /// redis缓存,使用1号数据库进行缓存
    /// </summary>
    [Injection(Pattern = InjectionPatterns.Self)]
    public class BlogRedisCacheAOP : CacheAOPbase, IScopedDependency
    {
        private IRedisCache _redisCache;
        public BlogRedisCacheAOP(IRedisCache redisCache)
        {
            _redisCache = redisCache;
            _redisCache.SetRedisDbNum(1);
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
            if (cachingAttribute != null)
            {
                //获取自定义缓存键
                var cacheKey = CustomCacheKey(invocation);
                //注意是 string 类型，方法GetValue
                var exist = _redisCache.KeyExists(cacheKey);
                if (exist)
                {
                    var cacheValue = _redisCache.Get<object>(cacheKey);
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
                        //dynamic _result = JsonConvert.DeserializeObject(cacheValue, returnType);
                        invocation.ReturnValue = isTask ? Task.FromResult(cacheValue) : cacheValue;
                        return;
                    }
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
                    _redisCache.Set<object>(cacheKey, response, TimeSpan.FromMinutes(cachingAttribute.AbsoluteExpiration));
                }
            }
            else
            {
                invocation.Proceed();//直接执行被拦截方法
            }
        }
    }
}
