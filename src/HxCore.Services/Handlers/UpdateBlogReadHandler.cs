using HxCore.IServices;
using HxCore.Model.NotificationHandlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HxCore.Services.Handlers
{
    /// <summary>
    /// 更新博客阅读次数处理程序
    /// </summary>
    public class UpdateBlogReadHandler : INotificationHandler<UpdateBlogReadModel>
    {
        private IBlogService _service;
        private readonly ILogger<UpdateBlogReadHandler> _logger;


        public UpdateBlogReadHandler(IServiceScopeFactory factory, ILogger<UpdateBlogReadHandler> logger)
        {
            _service = factory.CreateScope().ServiceProvider.GetService<IBlogService>();
            _logger = logger;
        }
        public async Task Handle(UpdateBlogReadModel notification, CancellationToken cancellationToken)
        {
            try
            {
                await _service.UpdateBlogReadAsync(notification);
            }
            catch (Exception ex)
            {
                _logger.LogError($"更新博客阅读次数失败，异常信息：{ex}");
            }
        }
    }
}
