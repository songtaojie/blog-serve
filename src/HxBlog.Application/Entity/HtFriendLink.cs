namespace HxBlog.Application.Entity;

/// <summary>
/// 友情链接
/// </summary>
[SugarTable(null, "友情链接表")]
[TenantBusiness]
[Tenant(ApplicationConst.ConfigId)]
public class HtFriendLink : EntityTenantBaseData
{
    /// <summary>
    /// 网站代码code
    /// </summary>
    [SugarColumn(ColumnDescription = "网站代码code", IsNullable = false, Length = 50)]
    public string SiteCode { get; set; }

    /// <summary>
    /// 网站名称
    /// </summary>
    [SugarColumn(ColumnDescription = "网站名称", IsNullable = false, Length = 50)]
    public string SiteName { get; set; }

    /// <summary>
    /// 网站链接
    /// </summary>
    [SugarColumn(ColumnDescription = "网站链接", Length = 500)]
    public string SiteUrl { get; set; }

    /// <summary>
    /// 网站logo
    /// </summary>
    [SugarColumn(ColumnDescription = "网站logo", Length = 500)]
    public string Logo { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnDescription = "排序")]
    public int Sort { get; set; }
}