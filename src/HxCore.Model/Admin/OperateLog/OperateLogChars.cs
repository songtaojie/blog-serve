using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Model.Admin.OperateLog
{
    /// <summary>
    /// 操作图表model
    /// </summary>
    public class OperateLogChars
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string[] columns { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object rows { get; set; }
    }

    /// <summary>
    /// 操作图表数据model
    /// </summary>
    public class OperateLogCharsData
    { 
        /// <summary>
        /// 访问接口的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 访问接口的
        /// </summary>
        public int Count { get; set; }
    }
}
