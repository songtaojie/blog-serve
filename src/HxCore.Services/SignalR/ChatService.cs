using Hx.Sdk.DependencyInjection;
using HxCore.IServices.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Services.SignalR
{
    public class ChatService : IChatService, ITransientDependency
    {
        private readonly IHubContext<ChatHub, IChatClient> _hubContext;

        public ChatService(IHubContext<ChatHub, IChatClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessage(string user, string message)
        {
            await _hubContext.Clients.All.ReceiveMessage(user, message);
        }

        //定于一个通讯管道，用来管理我们和客户端的连接
        //1、客户端调用 ReceiveUpdate，就像订阅
        public async Task ReceiveUpdate(string random)
        {
            //2、服务端主动向客户端发送数据，名字千万不能错
            await _hubContext.Clients.All.ReceiveUpdate("测试+" + random);
            //3、客户端再通过 ReceiveUpdate ，来接收
        }

    }
}
