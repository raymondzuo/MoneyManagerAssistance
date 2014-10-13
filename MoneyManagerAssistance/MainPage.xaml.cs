using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using MoneyManagerAssistance.Views;
using Raysoft.Database;
using Raysoft.Phone.Common;
using Raysoft.Utility;

namespace MoneyManagerAssistance
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : BasePage
    {
        private AppBarButton addNewAccountings;
        private AppBarButton budgetButton;
        private AppBarButton bbsSearchButton;
        private AppBarButton jifenButton;
        //private AppBarButton refreshButton;
        private AppBarButton settingButton;
        private AppBarButton loginOrOutButton;

        public MainPage()
        {
            this.InitializeComponent();

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
            // TODO: 准备此处显示的页面。

            // TODO: 如果您的应用程序包含多个页面，请确保
            // 通过注册以下事件来处理硬件“后退”按钮:
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed 事件。
            // 如果使用由某些模板提供的 NavigationHelper，
            // 则系统会为您处理该事件。

            //CreateDatabase.CreateLocalDatabase();
            //CreateDatabase.CreateAccountBookTable();
            //CreateDatabase.CreateAccountCategoryTable();
            //CreateDatabase.CreateAccountTable();
            //CreateDatabase.CreateMemberTable();
            //CreateDatabase.CreateSubAccountCategoryTable();
        }

        private void UIElement_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void MainPageHub_OnSectionsInViewChanged(object sender, SectionsInViewChangedEventArgs e)
        {
            if (this.BottomAppBar == null)
            {
                return;
            }

            //var bar = this.BottomAppBar as CommandBar;
            //bar.PrimaryCommands.Clear();


            
        }

        private void InitAppBar()
        {
            var appBar = new CommandBar();

            addNewAccountings = new AppBarButton() { Label = "添加账务" };
            addNewAccountings.Icon = new BitmapIcon() { UriSource = new Uri("ms-appx:///Assets/AppBarIcon/Add-New.png", UriKind.RelativeOrAbsolute) };
            addNewAccountings.Click += (sender, args) =>
            {
                NavigationService.Navigate(typeof(AccountPage));   
            };

            budgetButton = new AppBarButton() { Label = "财务预算" };
            budgetButton.Icon = new BitmapIcon() { UriSource = new Uri("ms-appx:///Assets/AppBarIcon/Budget.png", UriKind.RelativeOrAbsolute) };
            budgetButton.Click += (sender, args) =>
            {
                NavigationService.Navigate(typeof (BudgetPage));
            };

            appBar.PrimaryCommands.Add(addNewAccountings);
            appBar.PrimaryCommands.Add(budgetButton);
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
