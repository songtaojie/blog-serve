using Hx.Sdk.Common.Helper;
using Hx.Sdk.DatabaseAccessor;
using HxCore.Entity;
using HxCore.Entity.Entities;
using HxCore.Enums;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Model.Admin.Blog
{
    /// <summary>
    /// 博客创建所用模型
    /// </summary>
    
    public class BlogManageCreateModel : IAutoMapper<T_Blog>
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
        /// 内容，html格式
        /// </summary>
        public string ContentHtml { get; set; }
        /// <summary>
        /// 博客类型，是转发，原创，还是翻译等
        /// </summary> 
        public BlogType_Enum BlogType { get; set; } = BlogType_Enum.Original;
        /// <summary>
        /// 系统分类
        /// </summary>
        public string CategoryId
        {
            get; set;
        }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public string IsTop
        {
            get; set;
        } = ConstKey.No;
       
        /// <summary>
        /// 是否发布
        /// </summary>
        public string Publish
        {
            get; set;
        } = ConstKey.No;
        /// <summary>
        /// 是否发布
        /// </summary>
        public bool IsPublish => Helper.IsYes(Publish);
        /// <summary>
        /// 是否可评论
        /// </summary>
        public string CanCmt
        {
            get; set;
        } = ConstKey.Yes;
        /// <summary>
        /// 个人标签
        /// </summary>
        public List<string> BlogTags
        {
            get; set;
        }
        /// <summary>
        /// 是否是markdown
        /// </summary>
        public string MarkDown
        {
            get; set;
        } = ConstKey.No;

        /// <summary>
        /// 封面图
        /// </summary>
        public string CoverImgUrl
        {
            get; set;
        }
    }
}
