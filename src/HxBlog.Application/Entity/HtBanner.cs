namespace HxBlog.Application.Entity;

/// <summary>
/// Banner图
/// </summary>
[SugarTable(null, "Banner图表")]
[TenantBusiness]
[Tenant(ApplicationConst.ConfigId)]
public class HtBanner : EntityTenantBaseData
{
    /// <summary>
    /// 标题
    /// </summary>
    [SugarColumn(ColumnDescription = "标题", IsNullable = false, Length = 256)]
    public string Title { get; set; }

    /// <summary>
    /// 图片地址
    /// </summary>
    [SugarColumn(ColumnDescription = "图片地址", IsNullable = false, Length = 256)]
    public string ImgUrl { get; set; }

    /// <summary>
    /// 链接
    /// </summary>
    [SugarColumn(ColumnDescription = "链接", Length = 500)]
    public string Link { get; set; }

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