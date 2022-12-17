namespace HxBlog.Application.Entity;
/// <summary>
/// 公告通知
/// </summary>
[SugarTable(null, "公告通知表")]
[TenantBusiness]
[Tenant(ApplicationConst.ConfigId)]
public class HtNotice : EntityTenantBaseData
{
    /// <summary>
    /// 通知内容
    /// </summary>
    [SugarColumn(ColumnDescription = "通知内容", IsNullable = false, Length = 2000)]
    public string Content { get; set; }

    /// <summary>
    /// 链接
    /// </summary>
    [SugarColumn(ColumnDescription = "链接",  Length = 500)]
    public string Url { get; set; }

    /// <summary>
    /// 跳转方式,值为_Blank/_Self/_Parent/_Top
    /// </summary>
    [SugarColumn(ColumnDescription = "跳转方式,值为_Blank/_Self/_Parent/_Top", Length = 10)]
    public string Target { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnDescription = "排序")]
    public int Sort { get; set; }
}
