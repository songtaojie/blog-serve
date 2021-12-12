using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Model.Client
{
    /// <summary>
    /// 标签model
    /// </summary>
    public class TagModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 标签名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 背景颜色
        /// </summary>
        public string BGColor { get; set; }
    }
}
