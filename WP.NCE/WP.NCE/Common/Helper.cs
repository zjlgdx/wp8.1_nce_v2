using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace WP.NCE.Common
{
    class Helper
    {
        public async static Task ShowSystemTrayAsync(Color backgroundColor, Color foregroundColor, double opacity = 1, string text = "", bool isIndeterminate = true)
        {
            StatusBar statusBar = StatusBar.GetForCurrentView();
            statusBar.BackgroundColor = backgroundColor;
            statusBar.ForegroundColor = foregroundColor;
            statusBar.BackgroundOpacity = opacity;

            statusBar.ProgressIndicator.Text = text;
            if (!isIndeterminate)
            {
                statusBar.ProgressIndicator.ProgressValue = 0;
            }
            await statusBar.ProgressIndicator.ShowAsync();
        }

        public async static Task HideSystemTrayAsync(Color? originbackgroundColor, Color? originforegroundColor, double originopacity = 0)
        {
            StatusBar statusBar = StatusBar.GetForCurrentView();
            statusBar.BackgroundColor = originbackgroundColor;
            statusBar.ForegroundColor = originforegroundColor;
            statusBar.BackgroundOpacity = originopacity;
            await statusBar.ProgressIndicator.HideAsync();
        }

        /// <summary>
        /// http://msdn.microsoft.com/zh-cn/library/windows/apps/xaml/Dn263240(v=win.10).aspx
        /// 使用网络 API 时导致异常的原因包括以下几条：
        /// '参数验证错误
        /// '在查找主机名或 URI 时名称解析失败
        /// '网络连接中断
        /// '使用套接字和 HTTP 客户端 API 连接网络失败
        /// '网络服务器或远程终结点错误
        /// '各种网络错误
        /// C:\Program Files (x86)\Windows Phone Kits\8.1\Include\winerror.h
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string HandlerExceptionMessage(Exception ex)
        {
            switch (ex.HResult.ToString("X"))
            {
                case "80072EE7":
                    return Constants.WININET_E_NAME_NOT_RESOLVED;
                case "80072EE2":
                    return Constants.WININET_E_TIMEOUT;
                default:
                    break;
            }

            return Constants.UNKNOWN_ERROR;
        }
    }
}
