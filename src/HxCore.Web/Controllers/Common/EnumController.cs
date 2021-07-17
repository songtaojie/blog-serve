﻿using System.Collections.Generic;
using System.Linq;
using Hx.Sdk.DatabaseAccessor;
using HxCore.Entity;
using HxCore.Entity.Entities;
using HxCore.Model;
using HxCore.Web.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace HxCore.Web.Controllers
{
    /// <summary>
    /// 枚举的控制器
    /// </summary>
    public class EnumController : BaseAdminController
    {
        private IRepository<T_BlogType> _repository;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        public EnumController(IRepository<T_BlogType> repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// 获取博客类型的列表
        /// </summary>
        /// <returns></returns>
        [HttpPost, HttpGet]
        public List<BlogTypeModel> GetBlogTypeList()
        {
            var list = _repository.Where(b => b.Deleted == ConstKey.No)
                .Select(b => new BlogTypeModel
                {
                    Id = b.Id.ToString(),
                    Name = b.Name
                }).ToList();
            return list;
        }

        /// <summary>
        /// 获取博客类型的列表
        /// </summary>
        /// <returns></returns>
        [HttpPost, HttpGet]
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