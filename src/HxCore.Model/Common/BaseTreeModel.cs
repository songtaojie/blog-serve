using System;
using System.Collections.Generic;
using System.Text;

namespace HxCore.Model
{
    /// <summary>
    /// 树形结构基础model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseTreeModel<T>
    {
        /// <summary>
        /// id
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// 排序码
        /// </summary>
        public int SortNumber { get; set; }
        /// <summary>
        /// 父id
        /// </summary>
        public string ParentId { get; set; } = string.Empty;
        /// <summary>
        /// 子项
        /// </summary>
        public virtual List<T> Children { get; set; } = new List<T>(0);
    }
}
