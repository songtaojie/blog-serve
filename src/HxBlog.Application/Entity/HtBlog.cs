namespace HxBlog.Application.Entity;
/// <summary>
/// 博客
/// </summary>
[SugarTable(null, "博客表")]
[TenantBusiness]
[Tenant(ApplicationConst.ConfigId)]
public class HtBlog : EntityTenantBaseData
{
    /// <summary>
    /// 博客标题
    /// </summary>
    [SugarColumn(ColumnDescription = "博客标题", IsNullable = false, Length = 256)]
    public string Title { get; set; }

    /// <summary>
    /// 纯粹的内容，博客客户端列表简短的内容
    /// </summary>
    [SugarColumn(ColumnDescription = "纯粹的内容，博客客户端列表简短的内容", Length = 2000)]
    public string PureContent { get; set; }

    /// <summary>
    /// 是否使用MarkDown编辑的
    /// </summary>
    [SugarColumn(ColumnDescription = "是否使用MarkDown编辑的")]
    public bool IsMd { get; set; }

    /// <summary>
    /// 发布日期
    /// </summary>
    [SugarColumn(ColumnDescription = "发布日期")]
    public DateTime? PublishDate { get; set; }
    /// <summary>
    /// 置顶
    /// </summary>
    [SugarColumn(ColumnDescription = "置顶")]
    public bool IsTop { get; set; }

    /// <summary>
    /// 允许评论
    /// </summary>
    [SugarColumn(ColumnDescription = "允许评论")]
    public bool AllowCmt { get; set; }

    /// <summary>
    /// 阅读数
    /// </summary>
    [SugarColumn(ColumnDescription = "阅读数")]
    public int ReadNum { get; set; }

    /// <summary>
    /// 被收藏次数
    /// </summary>
    [SugarColumn(ColumnDescription = "被收藏次数")]
    public int FavNum { get; set; }

    /// <summary>
    /// 被评论次数
    /// </summary>
    [SugarColumn(ColumnDescription = "被评论次数")]
    public int CmtNum { get; set; }

    /// <summary>
    /// 热门程度
    /// 浏览一次+1，评论一次+2，收藏一次+5
    /// </summary>
    [SugarColumn(ColumnDescription = "热门程度")]
    public int OrderFactor { get; set; }

    /// <summary>
    /// 封面图url地址
    /// </summary>
    [SugarColumn(ColumnDescription = "封面图url地址", Length = 500)]
    public string CoverImgUrl { get; set; }

    /// <summary>
    /// 系统分类，前端、后端、编程语言等
    /// </summary> 
    [SugarColumn(ColumnDescription = "系统分类，前端、后端、编程语言等")]
    public long CategoryId { get; set; }

    /// <summary>
    /// 博客类型，是转发，原创，还是翻译等
    /// </summary> 
    [SugarColumn(ColumnDescription = "博客类型，是转发，原创，还是翻译等")]
    public BlogTypeEnum BlogType { get; set; } = BlogTypeEnum.None;

    /// <summary>
    /// 转载或者翻译需要有来源链接
    /// </summary>
    [SugarColumn(ColumnDescription = "转载或者翻译需要有来源链接", Length = 500)]
    public string SourceLink { get; set; }

    /// <summary>
    /// 博客状态
    /// </summary>
    [SugarColumn(ColumnDescription = "博客状态")]
    public BlogStatusEnum Status { get; set; }
}