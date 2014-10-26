using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供
using MoneyManagerAssistance.Views;
using Raysoft.Phone.Common;

namespace MoneyManagerAssistance.SubViews
{
    public sealed partial class Report : UserControl
    {
        public Report()
        {
            this.InitializeComponent();
        }

        private void UIElement_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var border = sender as Border;
            switch (border.Tag.ToString())
            {
                case "0":
                    NavigationService.Navigate(typeof(AccountDetailPage));
                    break;
                case "1":
                case "4":
                    NavigationService.Navigate(typeof(TrendPage));
                    break;
                case "2":
                    NavigationService.Navigate(typeof(StatisticsPage),2);
                    break;
                case "3":
                    NavigationService.Navigate(typeof(StatisticsPage),1);
                    break;
                case "5":
                    NavigationService.Navigate(typeof(CategoryPage));
                    break;
            }
            
            
        }
    }
}
