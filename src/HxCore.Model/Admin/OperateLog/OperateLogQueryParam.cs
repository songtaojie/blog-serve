﻿using Hx.Sdk.Entity.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.Model.Admin.OperateLog
{
    /// <summary>
    /// 操作记录查询参数
    /// </summary>
    public class OperateLogQueryParam: BasePageParam
    {
        /// <summary>
        /// 是否是欢迎页的日志列表
        /// </summary>
        public bool IsWelcome { get; set; }
    }
}
