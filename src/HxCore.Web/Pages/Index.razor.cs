using Hx.Sdk.Core;
using HxCore.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HxCore.Web.Pages
{
    public partial class Index: ComponentBase
    {
        [Inject]
        private IHxHttpClient Client { get; set; }

        [Parameter]
        public int PageIndex { get; set; } = 0;

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
            //Client.GetAsync<>;
            //blogList = result.Items;
        }
    }
}
