using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Model
{
    /// <summary>
    /// 文件上传参数
    /// </summary>
    public class UploadParam
    {
        /// <summary>
        /// 文件
        /// </summary>
        public IFormFile File { get; set; }
        /// <summary>
        /// 附件类型，0：文件，1：图片
        /// </summary>
        public AttachType AttachType { get; set; }

        /// <summary>
        /// 附件存储的路径，不设置使用默认路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 是否进行缩略图，仅对图片有效
        /// </summary>
        public bool? MakeThumbnail { get; set; } = false;

        /// <summary>
        /// 是否进行添加水印，仅图片
        /// </summary>
        public bool? MakeLetterWater { get; set; } = false;
    }

    /// <summary>
    /// 附件类型
    /// </summary>
    public enum AttachType
    { 
        /// <summary>
        /// 文件
        /// </summary>
        File = 0,
        /// <summary>
        /// 图片
        /// </summary>
        Image = 1
    }
}
