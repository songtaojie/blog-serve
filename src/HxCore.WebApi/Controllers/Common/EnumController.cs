using System.Collections.Generic;
using System.Linq;
using Hx.Sdk.Attributes;
using Hx.Sdk.DatabaseAccessor;
using HxCore.Entity;
using HxCore.Entity.Entities;
using HxCore.Model;
using HxCore.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HxCore.WebApi.Controllers
{
    /// <summary>
    /// 枚举的控制器
    /// </summary>
    [SkipRouteAuthorization]
    public class EnumController : BaseAdminController
    {
        private IRepository<T_Category> _repository;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        public EnumController(IRepository<T_Category> repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// 获取博客类型的列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, HttpPost]
        public List<CategoryModel> GetCategoryList()
        {
            var categoryRepository = _repository.Change<T_Category>();
            return categoryRepository.Where(b => b.Deleted == ConstKey.No)
                .Select(c => new CategoryModel
                {
                    Id = c.Id.ToString(),
                    Name = c.Name
                }).ToList();
        }
    }
}