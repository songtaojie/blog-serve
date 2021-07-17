using System;
using System.ComponentModel.DataAnnotations;

namespace HxCore.Entity.Entities
{
    /// <summary>
    /// 用户的基本信息
    /// </summary>
    public class T_BasicInfo : Hx.Sdk.DatabaseAccessor.EntityBase
    {
        /// <summary>
        /// 真实的名字
        /// </summary>
        [MaxLength(36)]
        public string RealName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [MaxLength(40)]
        public string CardId
        {
            set; get;
        }
        /// <summary>
        /// 生日
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? Birthday
        {
            set; get;
        }

        /// <summary>
        /// 性别
        /// </summary>
        [MaxLength(8)]
        public string Gender
        {
            set; get;
        }
        /// <summary>
        /// 用户的QQ
        /// </summary>
        [MaxLength(40)]
        public string QQ
        {
            get; set;
        }
        /// <summary>
        /// 用户微信号
        /// </summary>
        [MaxLength(40)]
        public string WeChat
        {
            get; set;
        }

        /// <summary>
        /// 电话
        /// </summary>
        [MaxLength(40)]
        public string Telephone
        {
            set; get;
        }
        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(40)]
        public string Mobile
        {
            get; set;
        }
        /// <summary>
        /// 自我描述
        /// </summary>
        [MaxLength(2000)]
        public string Description
        {
            set; get;
        }

        /// <summary>
        /// 用户地址
        /// </summary>
        [MaxLength(200)]
        public string Address
        {
            get; set;
        }
        /// <summary>
        /// 用户毕业学校
        /// </summary>
        [MaxLength(200)]
        public string School
        {
            get; set;
        }

        /// <summary>
        /// 用户的id
        /// </summary>
        [MaxLength(36)]
        public string UserId { get; set; }
    }
}
