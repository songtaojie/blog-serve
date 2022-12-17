namespace HxBlog.Entity.Entities;

/// <summary>
/// 博客扩展表
/// </summary>
[SugarTable(null, "博客扩展表")]
[TenantBusiness]
[Tenant(ApplicationConst.ConfigId)]
public class HtBlogExtend : EntityBaseId
{
    /// <summary>
    ///博客id
    /// </summary>
    [SugarColumn(ColumnDescription = "博客id")]
    public long BlogId { get; set; }

    /// <summary>
    /// 正文
    /// </summary>
    [SugarColumn(ColumnDescription = "正文", ColumnDataType = "longtext,text,clob")]
    public string Content { get; set; }

    /// <summary>
    /// 内容，html格式
    /// </summary>
    [SugarColumn(ColumnDescription = "内容，html格式", ColumnDataType = "longtext,text,clob")]
    public string ContentHtml { get; set; }
}
