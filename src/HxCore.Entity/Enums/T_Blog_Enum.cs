using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Enums
{
    /// <summary>
    /// 文章类型
    /// </summary>
    public enum T_BlogType_Enum
    {
        /// <summary>
        /// 原创
        /// </summary>
        [Description("原创")]
        Original = 0,

        /// <summary>
        /// 转载
        /// </summary>
        [Description("转载")]
        Reprint = 1,
        /// <summary>
        /// 翻译
        /// </summary>
        [Description("翻译")]
        Translation = 2
    }
}
