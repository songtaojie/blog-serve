using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Model.Admin.Banner
{
    /// <summary>
    /// 友情链接基础model
    /// </summary>
    public class BannerManageBaseModel
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
        /// 图片名字
        /// </summary>
        public string ImgFileName
        {
            get 
            { 
                if(string.IsNullOrEmpty(ImgUrl))return string.Empty;
                return Path.GetFileName(ImgUrl); 
            }
        }

        /// <summary>
        /// 链接
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 跳转方式，值为_Blank/_Self/_Parent/_Top
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderSort { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
