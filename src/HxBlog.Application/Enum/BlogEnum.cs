// Apache-2.0 License
// Copyright (c) 2021-2022 zuohuaijun
// 电话/微信：18020030720  QQ群：87333204

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxBlog.Application.Enum;
public enum BlogStatusEnum
{
    /// <summary>
    /// 草稿
    /// </summary>
    [Description("草稿")]
    Draft,
    /// <summary>
    /// 已发布
    /// </summary>
    [Description("已发布")]
    Publish
}

public enum BlogTypeEnum
{
    /// <summary>
    /// 未知
    /// </summary>
    [Description("未知")]
    None,
    /// <summary>
    /// 转发
    /// </summary>
    [Description("转发")]
    Forward,
    /// <summary>
    /// 原创
    /// </summary>
    [Description("原创")]
    Original,
    /// <summary>
    /// 翻译
    /// </summary>
    [Description("翻译")]
    Translate,
}
