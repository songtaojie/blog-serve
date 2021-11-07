using Hx.Sdk.DatabaseAccessor;
using HxCore.Entity;
using HxCore.Entity.Entities;
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
        public string BlogTypeId
        {
            get; set;
        }

        /// <summary>
        /// 系统分类
        /// </summary>
        public string CategoryId
        {
            get; set;
        }

        /// <summary>
        /// 博客标签
        /// </summary>
        public string BlogTags
        {
            get; set;
        }

        /// <summary>
        /// 个人置顶
        /// </summary>
        public string PersonTop
        {
            get; set;
        } = ConstKey.No;
        /// <summary>
        /// 仅自己可见
        /// </summary>
        public string Private
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
        /// 个人标签
        /// </summary>
        public List<BlogManagePersonTag> PersonTags { get; set; }
    }

    /// <summary>
    /// 博客个人标签
    /// </summary>
    public class BlogManagePersonTag
    { 
        /// <summary>
        /// 个人标签的id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 个人标签的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否可编辑
        /// </summary>
        public bool Editable { get; set; } = false;
    }
}
