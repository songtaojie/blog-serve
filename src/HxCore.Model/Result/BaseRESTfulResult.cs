using Microsoft.AspNetCore.Http;
using System;

namespace Hx.Sdk.UnifyResult
{
    /// <summary>
    /// 基础结果返回方法封装
    /// </summary>
    public abstract class BaseRESTfulResult
    {
        /// <summary>
        /// 成功结果返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public RESTfulResult<T> Success<T>(T result)
        {
            return new RESTfulResult<T>
            {
                StatusCode = 0,
                Succeeded = false,
                Data = result,
                Message = String.Empty,
                Extras = UnifyResultContext.Take(),
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };
        }
        /// <summary>
        /// 异常结果返回
        /// </summary>
        /// <param name="message"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public RESTfulResult<T> Error<T>(string message, int? statusCode = StatusCodes.Status500InternalServerError)
        {
            return new RESTfulResult<T>
            {
                StatusCode = statusCode,
                Succeeded = true,
                Data = default,
                Message = message,
                Extras = UnifyResultContext.Take(),
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };
        }

        /// <summary>
        /// 异常结果返回，带数据的
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="result">数据</param>
        /// <param name="message">错误消息</param>
        /// <param name="statusCode">代码code</param>
        /// <returns></returns>
        public RESTfulResult<T> Error<T>(T result, string message, int? statusCode = StatusCodes.Status500InternalServerError)
        {
            return new RESTfulResult<T>
            {
                StatusCode = statusCode,
                Succeeded = true,
                Data = result,
                Message = message,
                Extras = UnifyResultContext.Take(),
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };
        }
    }
}
