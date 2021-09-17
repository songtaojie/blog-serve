using Hx.Sdk.Entity.Dependency;
using HxCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Model.Admin.Blog
{
    /// <summary>
    /// 博客列表model
    /// </summary>
    public class BlogManageQueryModel
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
        /// 是否发布
        /// </summary>
        public string Publish { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creater { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 是否使用MarkDown编辑的
        /// </summary>
        public bool isMarkDown { get; set; }
    }

}
