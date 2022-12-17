namespace HxBlog.Application.Entity;

/// <summary>
/// 博客分类
/// </summary>
[SugarTable(null, "博客分类表")]
[TenantBusiness]
[Tenant(ApplicationConst.ConfigId)]
public class HtCategory : EntityTenantBaseData
{
    /// <summary>
    /// 分类名字
    /// </summary>
    [SugarColumn(ColumnDescription = "分类名字", IsNullable =false,Length =36)]
    public string Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(ColumnDescription = "描述", Length = 200)]
    public string Description { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnDescription = "排序", Length = 200)]
    public int Sort { get; set; }
}