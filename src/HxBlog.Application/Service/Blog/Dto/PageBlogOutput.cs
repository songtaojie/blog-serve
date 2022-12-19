// Apache-2.0 License
// Copyright (c) 2021-2022 zuohuaijun
// 电话/微信：18020030720  QQ群：87333204

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxBlog.Application.Service.Blog.Dto;
public class PageBlogOutput
{
    /// <summary>
    /// 博客标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 纯粹的内容，博客客户端列表简短的内容
    /// </summary>
    public string PureContent { get; set; }

    /// <summary>
    /// 是否使用MarkDown编辑的
    /// </summary>
    public bool IsMd { get; set; }

    /// <summary>
    /// 发布日期
    /// </summary>
    public DateTime? PublishDate { get; set; }
    /// <summary>
    /// 置顶
    /// </summary>
    public bool IsTop { get; set; }

    /// <summary>
    /// 允许评论
    /// </summary>
    public bool IsAllowCmt { get; set; }

    /// <summary>
    /// 阅读数
    /// </summary>
    public int ReadNum { get; set; }

    /// <summary>
    /// 被收藏次数
    /// </summary>
    public int FavNum { get; set; }

    /// <summary>
    /// 被评论次数
    /// </summary>
    public int CmtNum { get; set; }

    /// <summary>
    /// 热门程度
    /// 浏览一次+1，评论一次+2，收藏一次+5
    /// </summary>
    public int OrderFactor { get; set; }

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
