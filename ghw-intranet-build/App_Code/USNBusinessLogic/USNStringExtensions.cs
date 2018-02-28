using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace USNStarterKit.USNBusinessLogic
{

    /// <summary>
    /// Summary description for StringExtensions
    /// </summary>
    public static class USNStringExtensions
    {
        public static string FormatDateTime(this string dateString, string format)
        {
            DateTime dateTime;
            string str;
            str = (DateTime.TryParse(dateString, out dateTime) ? dateTime.ToString(format) : string.Empty);
            string str1 = str;
            return str1;
        }

        public static string ReplaceQueryStringItem(this string url, string key, string value)
        {
            string str;
            bool flag = url.Contains("?");
            if (flag)
            {
                flag = url.Contains(string.Concat(key, "="));
                str = (flag ? Regex.Replace(url, string.Concat("([?&]", key, ")=[^?&]+"), string.Concat("$1=", value)) : string.Format("{0}&{1}={2}", url, key, value));
            }
            else
            {
                str = string.Format("{0}?{1}={2}", url, key, value);
            }
            return str;
        }
    }

}