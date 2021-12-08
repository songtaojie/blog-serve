using Hx.Sdk.Core;
using HxCore.Model;
using HxCore.Web.Application;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HxCore.Web.Common;
using Microsoft.JSInterop;
using Microsoft.Extensions.Caching.Distributed;
using HxCore.Model.Client;

namespace HxCore.Web.Pages.Home
{
    public partial class Article : ComponentBase
    {
        [Inject]
        private BlogService Service { get; set; }

        [Inject]
        private IWebManager WebManager { get; set; }

        /// <summary>
        /// 缓存
        /// </summary>
        [Inject]
        private IDistributedCache Cache { get; set; }

        [Inject]
        private IJSRuntime JS { get; set; }

        [Parameter]
        public int PageIndex { get; set; }
       

        private IEnumerable<BlogQueryModel> blogList = new List<BlogQueryModel>();

        protected override async Task OnInitializedAsync()
        {
            await Articles();
            await base.OnInitializedAsync();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
        }
        public async Task Articles()
        {
            var result = await Service.GetArticleList(PageIndex);
            if(result!=null && result.Items !=null) blogList = result.Items;
        }
    }
}
