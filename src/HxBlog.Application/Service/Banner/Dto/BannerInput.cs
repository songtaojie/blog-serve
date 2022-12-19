// Apache-2.0 License
// Copyright (c) 2021-2022 zuohuaijun
// 电话/微信：18020030720  QQ群：87333204

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxBlog.Application.Service.Banner.Dto;
public class AddBannerInput
{
    /// <summary>
    /// 标题
    /// </summary>
    [Required(ErrorMessage ="Banner标题不能为空")]
    [MaxLength(256,ErrorMessage ="Banner标题长度不能大于{1}")]
    public string Title { get; set; }

    /// <summary>
    /// 图片地址
    /// </summary>
    [Required(ErrorMessage = "图片地址不能为空")]
    [MaxLength(256, ErrorMessage = "Banner标题长度不能大于{1}")]
    public string ImgUrl { get; set; }

    /// <summary>
    /// 链接
    /// </summary>
    [SugarColumn(ColumnDescription = "链接", Length = 500)]
    public string Url { get; set; }

    /// <summary>
    /// 跳转方式，值为_Blank/_Self/_Parent/_Top
    /// </summary>
    [SugarColumn(ColumnDescription = "跳转方式，值为_Blank/_Self/_Parent/_Top", Length = 10)]
    public string Target { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnDescription = "排序", Length = 10)]
    public int Sort { get; set; }
}
