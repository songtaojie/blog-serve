using Hx.Sdk.Common.Helper;
using Hx.Sdk.DatabaseAccessor;
using HxCore.Entity;
using HxCore.Entity.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Model.Client
{
    /// <summary>
    /// 博客视图模型
    /// </summary>
    public class BlogDetailModel:IAutoMapper<T_Blog>
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
        /// 博客内容
        /// </summary>
        public string Content
        {
            get; set;
        }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 是否是MarkDown语法
        /// </summary>
        [JsonIgnore]
        public string MarkDown { get; set; }

        /// <summary>
        /// 是否是MarkDown语法
        /// </summary>
        public bool IsMarkDown => Helper.IsYes(MarkDown);
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
        [JsonIgnore]
        public string Publish { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime? PublishDate { get; set; }
        /// <summary>
        /// 头像链接
        /// </summary>
        public string AvatarUrl { get; set; }
        /// <summary>
        /// 系统分类名称
        /// </summary>
        public string CategoryName { get; set; }

    }
    
}
