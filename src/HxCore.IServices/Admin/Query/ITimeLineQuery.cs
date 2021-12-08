﻿using HxCore.Model.Admin.TimeLine;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HxCore.IServices
{
    /// <summary>
    /// 时间轴查询类
    /// </summary>
    public interface ITimeLineQuery
    {
        /// <summary>
        /// 获取时间轴列表
        /// </summary>
        /// <returns></returns>
        Task<SqlSugarPageModel<TimeLineManageQueryModel>> QueryTimeLinePageAsync(TimeLineManageQueryParam param);

        /// <summary>
        /// 获取时间轴详情数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TimeLineManageDetailModel> GetDetailAsync(string id);
    }
}
