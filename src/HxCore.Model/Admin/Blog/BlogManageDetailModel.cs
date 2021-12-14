using Hx.Sdk.Common.Extensions;
using Hx.Sdk.DatabaseAccessor;
using HxCore.Entity;
using HxCore.Entity.Entities;
using HxCore.Enums;
using System.Collections.Generic;

namespace HxCore.Model.Admin.Blog
{
    /// <summary>
    /// 博客详情页数据
    /// </summary>
    public class BlogManageDetailModel : IAutoMapper<T_Blog>
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
        /// 封面图
        /// </summary>
        public string CoverImgUrl
        {
            get;set;
        }
        /// <summary>
        /// 文章类型
        /// </summary>
        public BlogType_Enum BlogType { get; set; }

        ///// <summary>
        ///// 文章类型-描述
        ///// </summary>
        //public string BlogType_V => BlogType.GetDescription();

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
        public string IsTop { get; set; } = ConstKey.No;

        /// <summary>
        /// 是否发布
        /// </summary>
        public string Publish
        {
            get; set;
        } = ConstKey.No;
        /// <summary>
        /// 是否可评论
        /// </summary>
        public string CanCmt
        {
            get; set;
        } = ConstKey.Yes;
       
        /// <summary>
        /// 是否是markdown
        /// </summary>
        public string MarkDown
        {
            get; set;
        } = ConstKey.No;

        /// <summary>
        /// 博客标签
        /// </summary>
        public List<string> BlogTags { get; set; }
    }
}
