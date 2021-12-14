using Castle.DynamicProxy;
using Hx.Sdk.Attributes;
using Hx.Sdk.Cache;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Hx.Sdk.DependencyInjection;
using HxCore.Entity;

namespace HxCore.Aops
{
    /// <summary>
    /// 客户端首页数据缓存
    /// </summary>
    public class HomeRedisCacheAOP : CacheAOPbase
    {
        private IRedisCache _redisCache;
        public HomeRedisCacheAOP(IRedisCache redisCache)
        {
            _redisCache = redisCache;
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
                var cacheKey = CustomCacheKey(invocation, CacheKeyConfig.HOME_KEY);
                //注意是 string 类型，方法GetValue
                var exist = _redisCache.KeyExists(cacheKey);
                if (exist)
                {
                    var cacheValue = _redisCache.StringGet(cacheKey);
                    if (!string.IsNullOrEmpty(cacheValue))
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
                        dynamic result = JsonConvert.DeserializeObject(cacheValue, returnType);
                        invocation.ReturnValue = isTask ? Task.FromResult(result) : result;
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
