namespace HxBlog.Application.Entity;
/// <summary>
/// 博客标签
/// </summary>
[SugarTable(null, "博客标签表")]
[TenantBusiness]
[Tenant(ApplicationConst.Blog_ConfigId)]
public class HtTag : EntityTenantBaseData
{
    /// <summary>
    /// 标签名字
    /// </summary>
    [SugarColumn(ColumnDescription = "标签名字", IsNullable = false, Length = 20)]
    public string Name { get; set; }

    /// <summary>
    /// 背景颜色
    /// </summary>
    [SugarColumn(ColumnDescription = "背景颜色",  Length = 20)]
    public string BGColor { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnDescription = "排序")]
    public int Sort { get; set; }
}