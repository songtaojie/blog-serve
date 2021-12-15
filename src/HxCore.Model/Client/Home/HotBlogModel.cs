using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Model.Client
{
    /// <summary>
    /// 热点文章
    /// </summary>
    public class HotBlogModel
    {
        /// <summary>
        /// 博客id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 博客标题
        /// </summary>
        public string Title
        {
            get; set;
        }
      
        /// <summary>
        /// 阅读量
        /// </summary>
        public long ReadCount { get; set; }

        /// <summary>
        /// 被评论次数
        /// </summary>
        public long CmtCount { get; set; }

        /// <summary>
        /// 封面图片链接
        /// </summary>
        public string CoverImgUrl { get; set; }
    }
}
