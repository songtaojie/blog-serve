using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Model.Client
{
    /// <summary>
    /// 时间戳model
    /// </summary>
    public class TimeLineModel
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 跳转方式，值为_Blank/_Self/_Parent/_Top
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? Timestamp { get; set; }
    }
}
