using Hx.Sdk.Core;
using HxCore.Model;
using HxCore.Web.Application;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HxCore.Web.Common;

namespace HxCore.Web.Pages.Home
{
    public partial class Article : ComponentBase
    {
        [Inject]
        private BlogService Service { get; set; }

        [Inject]
        private IWebManager WebManager { get; set; }

        private IEnumerable<BlogQueryModel> blogList = new List<BlogQueryModel>();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await Task.CompletedTask;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await Articles();
            await base.OnAfterRenderAsync(firstRender);
        }
        public async Task Articles()
        {
            //var result = await Service.GetArticleList(PageIndex);
            //blogList = result.Items;
        }
    }
}
