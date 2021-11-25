using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HxCore.Web.Common
{
    public static class WebHelper
    {
        /// <summary>
        /// 过滤html中的p标签
        /// </summary>
        /// <param name="html">html字符串</param>
        /// <param name="maxSize">返回的字符串最大长度为多少</param>
        /// <param name="onlyText">是否只返回纯文本，还是返回带有标签的</param>
        /// <returns></returns>
        public static string FilterHtmlP(string html, int maxSize, bool onlyText = true)
        {
            if (string.IsNullOrEmpty(html)) return "";
            Regex rReg = new Regex(@"<P>[\s\S]*?</P>", RegexOptions.IgnoreCase);
            var matchs = Regex.Matches(html, @"<P>[\s\S]*?</P>", RegexOptions.IgnoreCase);
            StringBuilder sb = new StringBuilder();
            foreach (Match match in matchs)
            {
                string pContent = match.Value;
                if (onlyText)
                {
                    pContent = Regex.Replace(pContent, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
                    pContent = Regex.Replace(pContent, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
                }
                sb.Append(pContent);
            }
            string result = sb.ToString();
            if (string.IsNullOrEmpty(result) || result.Length < maxSize) return result;
            return result.Substring(0, maxSize);
        }

        #region 日期处理函数
        public static string GetDispayDate(DateTime? date, bool showTime = false)
        {
            if (!date.HasValue) return "";
            return GetDispayDate(date.Value, showTime);
        }
        public static string GetDispayDate(DateTime date, bool showTime = false)
        {
            TimeSpan ts = DateTime.Now.Subtract(date);
            if (ts.TotalMinutes < 60) return string.Format("{0} 分钟前", (int)Math.Floor(ts.TotalMinutes));
            if (ts.TotalHours <= 24) return string.Format("{0} 小时前", (int)Math.Floor(ts.TotalHours));
            if (ts.TotalDays <= 7) return string.Format("{0} 天前", (int)Math.Floor(ts.TotalDays));
            if (date.Year == DateTime.Now.Year)
            {
                string timeFormat = showTime ? "MM-dd HH:ss" : "MM-dd";
                return date.ToString(timeFormat);
            }
            string format = showTime ? "yyyy-MM-dd HH:ss" : "yyyy-MM-dd";
            return date.ToString(format); ;
        }
        #endregion
    }
}
