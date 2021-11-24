using HxCore.IServices;
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
        private IBlogQuery BlogQuery { get; set; }

        [Parameter]
        public int PageIndex { get; set; } = 0;

        private IEnumerable<BlogQueryModel> blogList = new List<BlogQueryModel>();

        protected override async Task OnInitializedAsync()
        {
            await Articles();
            await base.OnInitializedAsync();
        }

        public async Task Articles()
        {
            var result = await BlogQuery.GetBlogsAsync(new Model.BlogQueryParam
            {
                PageIndex = PageIndex
            });
            blogList = result.Items;
        }
    }
}
