using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍
using MoneyManagerAssistance.SubViews;
using MoneyManagerAssistance.ViewModel;
using Raysoft.ModelLib;
using Raysoft.Phone.Common;

namespace MoneyManagerAssistance.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AccountDetailPage : BasePage
    {
        private AccountCategorySelector selector;
        private AccountDetailViewModel vm;


        public AccountDetailPage()
        {
            this.InitializeComponent();
            vm = new AccountDetailViewModel();
            this.DataContext = vm;
            
            this.Loaded += OnLoaded;
            
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var conditionDic = new Dictionary<string, string>();
            vm.SetStatisticsResult("DetailAll", conditionDic);
        }

        protected override void OnBackKeyPress(BackPressedEventArgs e)
        {
            if (this.selector != null && this.layout.Children.Contains(selector))
            {
                e.Handled = true;
            }
            else
            {
                base.OnBackKeyPress(e);
            }
        }



        private async void SaveAndBackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //await viewModel.GetAllAccountRecords();
        }
    }
}
