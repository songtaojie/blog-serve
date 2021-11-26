using HxCore.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HxCore.Entity.Entities
{
    /// <summary>
    /// 博客类
    /// </summary>
    public class T_Blog : Hx.Sdk.DatabaseAccessor.StatusEntityBase
    {
        /// <summary>
        /// 博客标题
        /// </summary>
        [MaxLength(200)]
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// 纯粹的内容，博客客户端列表简短的内容
        /// </summary>
        [MaxLength(1000)]
        public string PureContent { get; set; }

        /// <summary>
        /// 是否使用MarkDown编辑的
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string MarkDown { get; set; } = ConstKey.No;
       
        /// <summary>
        /// 是否发布，true代表发布，false代表不发布即是草稿
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string Publish { get; set; } = ConstKey.Yes;

        /// <summary>
        /// 发布日期
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? PublishDate { get; set; }
        /// <summary>
        /// 置顶 Y权值加10年
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string IsTop { get; set; } = ConstKey.No;
      
        /// <summary>
        /// 博客的个人标签，对应的是BlogTag表中主键，以，号隔开
        /// </summary>
        [MaxLength(40)]
        public string BlogTags { get; set; }

        /// <summary>
        /// 允许评论
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string CanCmt { get; set; } = ConstKey.Yes;
        /// <summary>
        /// 阅读量
        /// </summary>
        public long ReadCount { get; set; }
        /// <summary>
        /// 被收藏次数
        /// </summary>
        public long FavCount { get; set; }
        /// <summary>
        /// 被评论次数
        /// </summary>
        public long CmtCount { get; set; }
        /// <summary>
        /// 封面图url地址
        /// </summary>
        [MaxLength(255)]
        public string CoverImgUrl { get; set; }
        /// <summary>
        /// 系统分类，前端、后端、编程语言等
        /// </summary> 
        [MaxLength(36)]
        public string CategoryId { get; set; }
        /// <summary>
        /// 博客类型，是转发，原创，还是翻译等
        /// </summary> 
        public BlogType_Enum BlogType { get; set; }

        /// <summary>
        /// 转载或者翻译需要有来源链接
        /// </summary>
        public string SourceLink { get; set; }
    }
}
