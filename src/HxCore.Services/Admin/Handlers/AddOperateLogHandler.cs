using HxCore.IServices;
using HxCore.Model.NotificationHandlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HxCore.Services.Admin.Handlers
{
    /// <summary>
    /// 添加操作日志处理程序
    /// </summary>
    public class AddOperateLogHandler : INotificationHandler<AddOperateLogModel>
    {
        private IOperateLogService _operateLogService;
        private readonly ILogger<AddOperateLogHandler> _logger;


        public AddOperateLogHandler(IServiceScopeFactory factory,ILogger<AddOperateLogHandler> logger)
        {
            _operateLogService = factory.CreateScope().ServiceProvider.GetService<IOperateLogService>();
            _logger = logger;
        }
        public async Task Handle(AddOperateLogModel notification, CancellationToken cancellationToken)
        {
            try
            {
                await _operateLogService.AddOperateLog(notification);
            }
            catch (Exception ex)
            {
                _logger.LogError($"添加操作日志失败，异常信息：{ex}");
            }
        }
    }
}
