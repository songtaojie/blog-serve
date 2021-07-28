using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Model.Admin.Module
{
    /// <summary>
    /// 接口管理更新model
    /// </summary>
    public class ModuleUpdateModel: ModuleCreateModel
    {
        /// <summary>
        /// 接口的id
        /// </summary>
        public string Id { get; set; }
    }
}
