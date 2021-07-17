using Hx.Sdk.Common.Helper;
using Hx.Sdk.DatabaseAccessor;
using HxCore.Entity.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HxCore.Entity.Entities
{
    /// <summary>
    /// 路由菜单/按钮表
    /// </summary>
    public class T_Menu : Hx.Sdk.DatabaseAccessor.StatusEntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        public T_Menu()
        {
        }
        /// <summary>
        /// 父级菜单的id
        /// </summary>
        [MaxLength(36)]
        public string ParentId { get; set; }
        /// <summary>
        /// 菜单路径
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Path { get; set; }

        /// <summary>
        /// 菜单显示名（如左侧导航菜单名、或者按钮名称：编辑(按钮)、删除(按钮)）
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 菜单路由组件
        /// </summary>
        [MaxLength(50)]
        public string Component { get; set; }
       
        /// <summary>
        ///菜单类型；0：目录，1：菜单，2：按钮
        /// </summary>
        public T_Menu_Enum MenuType { get; set; }
        /// <summary>
        /// 是否是隐藏菜单
        /// </summary>
        public bool IsHide { get; set; } 
        /// <summary>
        /// 是否keepAlive
        /// </summary>
        public bool IskeepAlive { get; set; } 
        /// <summary>
        /// 菜单图标
        /// </summary>
        [MaxLength(100)]
        public string Icon { get; set; }
        /// <summary>
        /// 菜单描述    
        /// </summary>
        [MaxLength(200)]
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderSort { get; set; }
    }
}
