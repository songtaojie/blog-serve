using Hx.Sdk.DatabaseAccessor;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HxCore.Entity.Entities.Ids4
{
    /// <summary>
    /// 以下model 来自ids4项目，多库模式，为了调取ids4数据
    /// 角色表
    /// </summary>
    public class ApplicationRole : IdentityRole, IEntity<IdsDbContextLocator>
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string Deleted { get; set; } = "N";
        /// <summary>
        /// 描述说明
        /// </summary>
        [MaxLength(200)]
        public string Description { get; set; }
        /// <summary>
        ///排序
        /// </summary>
        public int OrderSort { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string Enabled { get; set; } = "Y";
        /// <summary>
        /// 创建ID
        /// </summary>
        [MaxLength(36)]
        public string CreaterId { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        [MaxLength(36)]
        public string Creater { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改ID
        /// </summary>
        [MaxLength(36)]
        public string LastModifierId { get; set; }
        /// <summary>
        /// 修改者
        /// </summary>
        [MaxLength(36)]
        public string LastModifier { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; } = DateTime.Now;

        #region 操作
        /// <summary>
        /// 设置创建者信息
        /// </summary>
        /// <param name="createrId">创建者Id</param>
        /// <param name="creater">创建者名称</param>
        public void SetCreater(string createrId, string creater)
        {
            this.CreaterId = createrId;
            this.Creater = creater;
            this.CreateTime = DateTime.Now;
            SetModifier(createrId, creater);
        }

        /// <summary>
        /// 设置创建者信息
        /// </summary>
        /// <param name="modifierId">更新者Id</param>
        /// <param name="modifier">更新者名称</param>
        public void SetModifier(string modifierId, string modifier)
        {
            this.LastModifierId = modifierId;
            this.LastModifier = modifier;
            this.LastModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="deleterId">设置删除者id</param>
        /// <param name="deleter">删除者名称</param>
        public void SetDelete(string deleterId, string deleter)
        {
            this.Deleted = "Y";
            SetModifier(deleterId, deleter);
        }
        #endregion

    }
}
