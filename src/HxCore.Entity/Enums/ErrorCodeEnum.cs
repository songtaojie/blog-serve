using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HxCore.Entity.Enum
{
    /// <summary>
    /// 错误码枚举
    /// </summary>
    public enum ErrorCodeEnum
    {
        /// <summary>
        /// 未知错误
        /// </summary>
        [Description("未知错误")]
        UnknownError = 90000,
        /// <summary>
        /// 通用错误码
        /// </summary>
        [Description("错误")]
        Error = 99999,
        /// <summary>
        /// 系统内部出错
        /// </summary>
        [Description("系统内部出错")]
        SystemError = 99991,
        #region 通用错误
        /// <summary>
        /// 空参数
        /// </summary>
        [Description("空参数")]
        ParamsNullError = 90001,
        /// <summary>
        /// 无效参数
        /// </summary>
        [Description("无效参数")]
        ParamsInValidError = 90002,
        /// <summary>
        /// 参数缺失
        /// </summary>
        [Description("参数缺失")]
        ParamsLackError = 90003,
        /// <summary>
        /// 无权访问
        /// </summary>
        [Description("无权访问")]
        NoAccessError = 90004,
        /// <summary>
        /// 添加失败
        /// </summary>
        [Description("添加失败")]
        AddError = 90005,
        /// <summary>
        /// 更新失败
        /// </summary>
        [Description("更新失败")]
        UpdateError = 90006,
        /// <summary>
        /// 删除失败
        /// </summary>
        [Description("删除失败")]
        DeleteError = 90007,
        /// <summary>
        ///空数据
        /// </summary>
        [Description("空数据")]
        DataNull = 90008,
        /// <summary>
        /// 第三方接口调用失败
        /// </summary>
        [Description("第三方接口调用失败")]
        ThirdApiError = 90009,
        #endregion
    }
}
