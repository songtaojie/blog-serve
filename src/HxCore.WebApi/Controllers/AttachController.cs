﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Hx.Sdk.Common.Helper;
using Hx.Sdk.Core;
using Hx.Sdk.ImageSharp;
using HxCore.Model;
using HxCore.Options;
using HxCore.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HxCore.WebApi.Controllers
{
    /// <summary>
    /// 附件控制器
    /// </summary>
    //[ApiExplorerSettings(IgnoreApi =true)]
    public class AttachController : BaseApiController
    {
        private readonly IWebManager webManager;
        private readonly IUserContext userContext;
        private readonly AttachSettingsOptions _attachSettings;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="webManager"></param>
        /// <param name="userContext"></param>
        /// <param name="options">附件配置</param>
        public AttachController(IWebManager webManager,IUserContext userContext,IOptions<AttachSettingsOptions> options)
        {
            this.webManager = webManager;
            this.userContext = userContext;
            _attachSettings = options.Value;
        }

        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="param">上传的文件</param>
        /// <returns></returns>
        [HttpPost]
        [SkipUnify]
        public Dictionary<string, object> Upload([FromForm] UploadParam param)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("successed", false);
            ErrorHelper.ThrowIfNull(param.File, "请上传内容!");
            string fileName = param.File.FileName;
            string fileExt = Path.GetExtension(fileName);
            //判断是否是图片类型
            if (param.AttachType == AttachType.Image) ErrorHelper.ThrowIfTrue(!ImageManager.IsImage(fileExt), "请上传图片文件!");
            ////最大文件大小
            long maxSize = _attachSettings.MaxSize.Value;
            if (param.File.Length > maxSize) return Error(string.Format("上传文件大小超过限制,最大上传[{0}]!", FileHelper.GetFileSizeDes(maxSize)));
            //路径处理
            string rootPath = _attachSettings.RootPath;
            if (string.IsNullOrEmpty(param.Path))
            {
                string userId = userContext.IsAuthenticated ? userContext.UserId : "Anonymous";
                param.Path = userId + "/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            }
            
            if (Path.GetInvalidPathChars().Any(p => param.Path.Contains(p))) return Error("上传文件失败!，路径包含非法字符");
            string dirPath = rootPath + "/" + param.Path;
            //绝对路径
            string mapRootPath = Path.Combine(webManager.WebRootPath, dirPath);
            FileHelper.TryCreateDirectory(mapRootPath);
            //文件名
            if (string.IsNullOrEmpty(param.FileName))
            {
                param.FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond;
            }
            if(Path.GetInvalidFileNameChars().Any(p => param.FileName.Contains(p))) return Error("上传文件失败!，文件名包含非法字符");
            //文件全路径
            //源文件
            string sourceFileName = param.FileName + fileExt;
            string sourceFilePath = Path.Combine(mapRootPath, sourceFileName);
            using (FileStream fs = System.IO.File.Create(sourceFilePath))
            {
                param.File.CopyTo(fs);
                fs.Flush();
            }
            string thumbnailPath = sourceFilePath;
            var fontOptions = new Hx.Sdk.ImageSharp.Fonts.FontOptions(_attachSettings.Image.Letter);
            var makeLetterWater = _attachSettings.Image.MakeLetterWater == true && param.MakeLetterWater == true;
            var makeThumbnail = _attachSettings.Image.MakeThumbnail == true && param.MakeThumbnail == true;
            //源文件添加水印
            if (makeLetterWater)ImageManager.MarkLetterWater(sourceFilePath, fontOptions);
            if (makeThumbnail)
            {
                ImageManager.GetImageSize(sourceFilePath, out int imgWidth, out int imgHeight);
                int tw = _attachSettings.Image.ThumbnailW;
                int th = _attachSettings.Image.ThumbnailH;
                if (imgWidth > tw && imgHeight > th && tw>0 && th > 0)
                {
                    thumbnailPath = Path.Combine(mapRootPath, string.Format("{0}_{1}x{2}{3}", param.FileName, tw, th, fileExt));
                    ImageManager.MakeThumbnail(sourceFilePath, thumbnailPath, tw, th, ThumbnailMode.Max);
                    //缩略图添加水印
                    if (makeLetterWater)ImageManager.MarkLetterWater(thumbnailPath, fontOptions);
                }
            }
            string fullUrl = webManager.GetFullUrl(_attachSettings.BaseUrl, webManager.ToRelativePath(sourceFilePath));
            string thumFullUrl = webManager.GetFullUrl(_attachSettings.BaseUrl, webManager.ToRelativePath(thumbnailPath));
            result["successed"] = true;
            result["url"] = fullUrl;
            result["thumUrl"] = thumFullUrl;
            return result;
        }

        private Dictionary<string, object> Error(string message)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result["successed"] = false;
            result.Add("message", message);
            return result;
        }
        /// <summary>
        /// 获取md编辑器的模板内容
        /// </summary>
        /// <returns></returns>
        [HttpGet,HttpPost]
        public string GetMdTemplate()
        {
            return FileHelper.GetString(Path.Combine(webManager.WebRootPath, "static/template.md"));
        }

    }
}