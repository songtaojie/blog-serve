using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Model.NotificationHandlers
{
    /// <summary>
    ///更新博客阅读次数
    /// </summary>
    public class UpdateBlogReadModel : INotification
    {
        /// <summary>
        /// 博客id
        /// </summary>
        public string Id { get; set; }
    }
}
