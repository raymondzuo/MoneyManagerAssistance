using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍
using MoneyManagerAssistance.ViewModel;
using Raysoft.Phone.Common;
using System.ComponentModel;
using Raysoft.Utility;

namespace MoneyManagerAssistance.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CategoryPage : BasePage
    {
        private AccoutStatisticsViewModel vm;
        private AppBarButton addNewAccountings;
        public CategoryPage()
        {
            this.InitializeComponent();
            vm = new AccoutStatisticsViewModel();
            this.MainGrid.DataContext = vm;
            InitAppBar();
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
            vm.SetStatisticsResult("MemberId", conditionDic);
        }

        private void InitAppBar()
        {
            var appBar = new CommandBar();

            addNewAccountings = new AppBarButton() { Label = "查看大类" };
            addNewAccountings.Icon = new BitmapIcon() { UriSource = new Uri("ms-appx:///Assets/AppBarIcon/Add-New.png", UriKind.RelativeOrAbsolute) };
            addNewAccountings.Click += (sender, args) =>
            {


            };

            appBar.PrimaryCommands.Add(addNewAccountings);
            this.BottomAppBar = appBar;
            SetAppBarBackgroundColor();
        }

        public void SetAppBarBackgroundColor(double Opacity = 0.88)
        {
            if (this.BottomAppBar != null)
            {
                this.BottomAppBar.Opacity = Opacity;
                this.BottomAppBar.IsSticky = false;
                this.BottomAppBar.Foreground = new SolidColorBrush(Utility.ConvertColorFromHex("#3B79A9"));
            }
        }
    }
}
