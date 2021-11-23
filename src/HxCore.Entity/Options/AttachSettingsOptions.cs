using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace HxCore.Options
{
    /// <summary>
    /// 附件上传配置
    /// </summary>
    public class AttachSettingsOptions : IPostConfigureOptions<AttachSettingsOptions>
    {
        /// <summary>
        /// 根路径
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        /// 文件的baseurl，api项目的域名
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// 图片配置
        /// </summary>
        public ImageSettings Image { get; set; }

        /// <summary>
        /// 后置配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="name"></param>
        public void PostConfigure(string name, AttachSettingsOptions options)
        {
            RootPath ??= "fileupload";
            Image ??= new ImageSettings
            {
                MaxSize = 2048,
                MakeThumbnail = false,
                MakeLetterWater = false
            };
        }
    }

    /// <summary>
    /// 图片的设置
    /// </summary>
    public class ImageSettings
    {
        /// <summary>
        /// 允许上传的图片的格式
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public long? MaxSize { get; set; }

        /// <summary>
        /// 是否可以缩略图
        /// </summary>
        public bool? MakeThumbnail { get; set; }

        /// <summary>
        /// 缩略图的宽度
        /// </summary>
        public int ThumsizeW { get; set; }

        /// <summary>
        /// 缩略图的高度
        /// </summary>
        public int ThumsizeH { get; set; }

        /// <summary>
        /// 是否添加水印
        /// </summary>
        public bool? MakeLetterWater { get; set; }

        /// <summary>
        /// 水印内容
        /// </summary>
        public string Letter { get; set; }
    }
}
