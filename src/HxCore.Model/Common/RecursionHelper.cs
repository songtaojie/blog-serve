using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HxCore.Model
{
    /// <summary>
    /// 递归帮助类
    /// </summary>
    public class RecursionHelper
    {
        /// <summary>
        /// 递归公共方法
        /// </summary>
        /// <typeparam name="T">要递归的类</typeparam>
        /// <param name="allMenus">要递归的类集合</param>
        /// <param name="parentId">递归的根节点</param>
        /// <returns>递归后的集合</returns>
        public static List<T> HandleTreeChildren<T>(IEnumerable<T> allMenus, string parentId = null) where T : BaseTreeModel<T>
        {

            if (allMenus == null || !allMenus.Any()) return new List<T>();
            List<T> parentMenus = new List<T>();
            if (string.IsNullOrEmpty(parentId))
            {
                parentMenus = allMenus.Where(m => string.IsNullOrEmpty(m.ParentId)).ToList();
            }
            else
            {
                parentMenus = allMenus.Where(m => m.ParentId == parentId).ToList();
            }

            if (parentMenus.Any())
            {
                parentMenus.ForEach(p =>
                {
                    p.Children = HandleTreeChildren(allMenus, p.Id);
                });
            }
            return parentMenus;
        }
    }
}
