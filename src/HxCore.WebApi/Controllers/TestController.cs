using HxCore.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Hx.Sdk.EventBus;
using HxCore.Model;
using HxCore.IServices;

namespace HxCore.WebApi.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    public class TestController: BaseApiController
    {
        private IEventBus _eventBus;
        private IMenuQuery _menuQuery;
        /// <summary>
        /// 控制器
        /// </summary>
        /// <param name="eventBus"></param>
        public TestController(IMenuQuery menuQuery)
        {
            _menuQuery = menuQuery;
        }
        [HttpGet]
        public async void TestSqlSugur()
        {
            await _menuQuery.GetDetailAsync("");
        }

        /// <summary>
        /// 测试发布
        /// </summary>
        /// <param name="basePush"></param>
        /// <returns></returns>
        [HttpPost]
        public BasePush PublishTest(BasePush basePush)
        {
            return basePush;
        }

        /// <summary>
        /// 测试接受
        /// </summary>
        /// <param name="basePush"></param>
        /// <returns></returns>
        [HttpPost]
        [DotNetCore.CAP.CapSubscribe("Hx.Sdk.Cap.Test")]
        public BasePush SubscribeTest(BasePush basePush)
        {
            return basePush;
        }
    }
}
