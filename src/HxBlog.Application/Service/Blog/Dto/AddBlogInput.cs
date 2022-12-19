// Apache-2.0 License
// Copyright (c) 2021-2022 zuohuaijun
// 电话/微信：18020030720  QQ群：87333204

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxBlog.Application.Service.Blog.Dto;
public class AddBlogInput
{
    /// <summary>
    /// 博客标题
    /// </summary>
    [Required(ErrorMessage ="博客标题不能为空")]
    [MaxLength(256,ErrorMessage ="博客标题长度不能大于{1}")]
    public string Title { get; set; }

    /// <summary>
    /// 是否使用MarkDown编辑的
    /// </summary>
    public bool IsMd { get; set; }

    /// <summary>
    /// 置顶
    /// </summary>
    public bool IsTop { get; set; }

    /// <summary>
    /// 允许评论
    /// </summary>
    public bool AllowCmt { get; set; }

    /// <summary>
    /// 封面图url地址
    /// </summary>
    public string CoverImgUrl { get; set; }

    /// <summary>
    /// 系统分类，前端、后端、编程语言等
    /// </summary> 
    public long CategoryId { get; set; }

    /// <summary>
    /// 博客类型，是转发，原创，还是翻译等
    /// </summary> 
    public BlogTypeEnum BlogType { get; set; } = BlogTypeEnum.None;

    /// <summary>
    /// 转载或者翻译需要有来源链接
    /// </summary>
    public string SourceLink { get; set; }

    /// <summary>
    /// 博客状态
    /// </summary>
    public BlogStatusEnum Status { get; set; }
}
