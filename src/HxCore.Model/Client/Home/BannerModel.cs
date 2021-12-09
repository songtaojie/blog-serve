using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Model.Client
{
    /// <summary>
    /// 首页横幅查询参数
    /// </summary>
    public class BannerModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 跳转方式，值为_Blank/_Self/_Parent/_Top
        /// </summary>
        public string Target { get; set; }
    }
}
