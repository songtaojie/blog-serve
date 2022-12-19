using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HxBlog.Application.Entity;
/// <summary>
/// 时间轴
/// </summary>
[SugarTable(null, "时间轴表")]
[TenantBusiness]
[Tenant(ApplicationConst.Blog_ConfigId)]
public class HtTimeLine : EntityTenantBaseData
{
    /// <summary>
    /// 内容
    /// </summary>
    [SugarColumn(ColumnDescription = "内容", IsNullable = false, Length = 1000)]
    public string Content { get; set; }
    /// <summary>
    /// 链接
    /// </summary>
    [SugarColumn(ColumnDescription = "链接",Length = 500)]
    public string Url { get; set; }

    /// <summary>
    /// 跳转方式，值为_Blank/_Self/_Parent/_Top
    /// </summary>
    [SugarColumn(ColumnDescription = "跳转方式，值为_Blank/_Self/_Parent/_Top", Length = 10)]
    public string Target { get; set; }
}