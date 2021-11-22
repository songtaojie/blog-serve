using Hx.Sdk.Common.Helper;
using HxCore.IServices.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Services.SignalR
{
    [Authorize]
    public class ChatHub : Hub<IChatClient>
    {
        /// <summary>
        /// 客户端重连接时
        /// </summary>-
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            ConsoleHelper.WriteInfoLine("连接ID    " + Context.ConnectionId);
            ConsoleHelper.WriteInfoLine("userId    " + Context.UserIdentifier);
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// 断线
        /// </summary>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            /*
              如果客户端故意断开连接（ connection.stop() 例如，通过调用），则 exception 参数将为 null 。
            但是，如果客户端由于错误（例如网络故障）而断开连接，则 exception 参数将包含描述失败的异常。
             */
            ConsoleHelper.WriteInfoLine("connId:    " + Context.ConnectionId + "     userId: " + Context.UserIdentifier + "     断开连接了");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.ReceiveMessage(user, message);
        }

        //定于一个通讯管道，用来管理我们和客户端的连接
        //1、客户端调用 ReceiveUpdate，就像订阅
        public async Task ReceiveUpdate(string random)
        {
            //2、服务端主动向客户端发送数据，名字千万不能错
            await Clients.All.ReceiveUpdate("测试+" + random);
            //3、客户端再通过 ReceiveUpdate ，来接收
        }
    }
}
