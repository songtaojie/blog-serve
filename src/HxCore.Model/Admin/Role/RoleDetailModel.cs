using Hx.Sdk.DatabaseAccessor;
using HxCore.Entity.Entities;
using System.Collections.Generic;

namespace HxCore.Model.Admin.Role
{
    /// <summary>
    /// 角色详情
    /// </summary>
    public class RoleDetailModel : IAutoMapper<T_Role>
    {
        /// <summary>
        /// 接口的id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 角色Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// /描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderSort { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 所拥有的的菜单的权限id
        /// </summary>
        public List<string> MenuIds { get; set; }
    }
}
