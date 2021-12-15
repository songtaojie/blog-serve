using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Model.Client
{
    /// <summary>
    /// 主页上所有数据
    /// </summary>
    public class HomeAllDataModel
    {
        /// <summary>
        /// 首页横幅列表数据
        /// </summary>
        public List<BannerModel> Banners { get; set; }

        /// <summary>
        /// 首页通知公告列表数据
        /// </summary>
        public List<NoticeModel> Notices { get; set; }

        /// <summary>
        /// 首页友情链接列表数据
        /// </summary>
        public List<FriendLinkModel> FriendLinks { get; set; }

        /// <summary>
        /// 标签列表数据
        /// </summary>
        public List<TagModel> Tags { get; set; }

        /// <summary>
        /// 热点文章
        /// </summary>
        public List<HotBlogModel> Hots { get; set; }
    }
}
