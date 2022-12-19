// Apache-2.0 License
// Copyright (c) 2021-2022 zuohuaijun
// 电话/微信：18020030720  QQ群：87333204

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxBlog.Application.Service.Banner.Dto;
public class PageBannerOutput
{
    /// <summary>
    /// 主键id
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 图片地址
    /// </summary>
    public string ImgUrl { get; set; }

    /// <summary>
    /// 链接
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 跳转方式，值为_Blank/_Self/_Parent/_Top
    /// </summary>
    public string Target { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }
}
