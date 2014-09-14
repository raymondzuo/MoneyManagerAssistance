using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace Raysoft.Common
{
    public class BasePage:Page
    {
        public BasePage()
        {
            StatusBar.GetForCurrentView().HideAsync();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
    }
}
