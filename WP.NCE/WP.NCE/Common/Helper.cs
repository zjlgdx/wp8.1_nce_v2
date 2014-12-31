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
        public async static Task ShowSystemTrayAsync(Color backgroundColor, Color foregroundColor,
   double opacity = 1, string text = "", bool isIndeterminate = true)
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

        public async static Task HideSystemTrayAsync(Color? originbackgroundColor, Color? originforegroundColor,
   double originopacity = 0)
        {
            StatusBar statusBar = StatusBar.GetForCurrentView();
            statusBar.BackgroundColor = originbackgroundColor;
            statusBar.ForegroundColor = originforegroundColor;
            statusBar.BackgroundOpacity = originopacity;


            await statusBar.ProgressIndicator.HideAsync();
        }
    }
}
