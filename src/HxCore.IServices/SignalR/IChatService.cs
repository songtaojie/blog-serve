using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.IServices.SignalR
{
    /// <summary>
    /// 推送消息服务
    /// </summary>
    public interface IChatService
    {
        Task SendMessage(string user, string message);
    }
}
