using Hx.Sdk.Common.Helper;
using Hx.Sdk.DatabaseAccessor;
using HxCore.Entity.Entities;
using HxCore.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Model.Client
{
    /// <summary>
    /// 博客查询实体类
    /// </summary>
    public class BlogQueryModel:IAutoMapper<T_Blog>
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
        /// 置顶
        /// </summary>
        [JsonIgnore]
        public string Top { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop => Helper.IsYes(Top);

        /// <summary>
        /// 博客类型，是转发，原创，还是翻译等
        /// </summary> 
        public BlogType_Enum BlogType { get; set; }
        /// <summary>
        /// 发布者
        /// </summary>
        public string Publisher { get; set; }
        /// <summary>
        /// 纯粹的内容，首页列表需要显示的内容
        /// </summary>
        public string PureContent
        {
            get;set;
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
        /// 发布日期
        /// </summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// 封面图片链接
        /// </summary>
        public string CoverImgUrl { get; set; }

        /// <summary>
        /// 头像链接
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 博客标签
        /// </summary>
        public IEnumerable<TagModel> Tags { get; set; }
    }

   
}
