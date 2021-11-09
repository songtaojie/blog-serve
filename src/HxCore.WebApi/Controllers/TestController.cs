using HxCore.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Hx.Sdk.EventBus;
using HxCore.Model;

namespace HxCore.WebApi.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    public class TestController: BaseApiController
    {
        private IEventBus _eventBus;
        /// <summary>
        /// 控制器
        /// </summary>
        /// <param name="eventBus"></param>
        public TestController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }


        [HttpPost]
        public BasePush PublishTest(BasePush basePush)
        {
            return basePush;
        }

        [HttpPost]
        [DotNetCore.CAP.CapSubscribe("Hx.Sdk.Cap.Test")]
        public BasePush SubscribeTest(BasePush basePush)
        {
            return basePush;
        }
    }
}
