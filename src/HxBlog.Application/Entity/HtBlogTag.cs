namespace HxBlog.Application.Entity;
/// <summary>
/// 博客标签映射表
/// </summary>
[SugarTable(null, "博客标签映射表")]
[TenantBusiness]
[Tenant(ApplicationConst.ConfigId)]
public class HtBlogTag : EntityBaseId
{
    /// <summary>
    /// 博客id
    /// </summary>
    [SugarColumn(ColumnDescription = "博客id")]
    public long BlogId { get; set; }

    /// <summary>
    /// 标签id
    /// </summary>
    [SugarColumn(ColumnDescription = "标签id")]
    public long TagId { get; set; }
}
