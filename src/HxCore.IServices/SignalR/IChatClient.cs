using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.IServices.SignalR
{
    /// <summary>
    ///  强类型配置 客户端方法
    /// </summary>
    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message);

        Task ReceiveUpdate(string message);
    }
}
