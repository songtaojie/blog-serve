using HxCore.Model;
using HxCore.Model.Client;
using HxCore.Web.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HxCore.Web.Pages.Home
{
    public partial class Index : ComponentBase
    {
        [Inject]
        private BlogService Service { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Parameter]
        public int PageIndex { get; set; } = 0;

        private IEnumerable<BlogQueryModel> blogList = new List<BlogQueryModel>();

        protected override async Task OnInitializedAsync()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            QueryHelpers.ParseQuery(uri.Query).TryGetValue("p", out Microsoft.Extensions.Primitives.StringValues p);
            Console.WriteLine(NavigationManager.Uri);
            int.TryParse(p, out int pageIndex);
            PageIndex = pageIndex;
            await base.OnInitializedAsync();
            await Task.CompletedTask;
        }
    }
}
