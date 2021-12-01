using Hx.Sdk.DatabaseAccessor;
using HxCore.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Model.Admin.Blog
{
    /// <summary>
    /// 博客标签model
    /// </summary>
    public class TagModel : IAutoMapper<T_TagInfo>
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

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderSort { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
