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
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace MoneyManagerAssistance
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private AppBarButton checkInButton;
        private AppBarButton marktReviewButton;
        private AppBarButton bbsSearchButton;
        private AppBarButton jifenButton;
        //private AppBarButton refreshButton;
        private AppBarButton settingButton;
        private AppBarButton loginOrOutButton;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            StatusBar.GetForCurrentView().BackgroundOpacity = 0;
            InitAppBar();
            
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: 准备此处显示的页面。

            // TODO: 如果您的应用程序包含多个页面，请确保
            // 通过注册以下事件来处理硬件“后退”按钮:
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed 事件。
            // 如果使用由某些模板提供的 NavigationHelper，
            // 则系统会为您处理该事件。
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

            var bar = this.BottomAppBar as CommandBar;
            bar.PrimaryCommands.Clear();


            
        }

        private void InitAppBar()
        {
            CommandBar appBar = new CommandBar();

            checkInButton = new AppBarButton() { Label = "添加账务" };
            checkInButton.Icon = new BitmapIcon() { UriSource = new Uri("ms-appx:///Assets/AppBarIcon/Add-New.png", UriKind.Absolute) };
            appBar.PrimaryCommands.Add(checkInButton);

            marktReviewButton = new AppBarButton() { Label = "好评" };
            marktReviewButton.Icon = new SymbolIcon(Symbol.Like);

            bbsSearchButton = new AppBarButton() { Label = "论坛搜索" };
            bbsSearchButton.Icon = new SymbolIcon(Symbol.Zoom);

            jifenButton = new AppBarButton() { Label = "积分" };
            //jifenButton.Icon = new SymbolIcon(Symbol.Favorite);
            jifenButton.Icon = new BitmapIcon() { UriSource = new Uri("ms-appx:///Assets/AppBarIcon/Add-New.png", UriKind.RelativeOrAbsolute) };

            //refreshButton = new AppBarButton() { Label = "刷新" };
            //refreshButton.Icon = new SymbolIcon(Symbol.Refresh);
            //refreshButton.Click += refreshButton_Click;

            settingButton = new AppBarButton() { Label = "设置" };
            settingButton.Icon = new SymbolIcon(Symbol.Setting);

            loginOrOutButton = new AppBarButton();
            //loginOrOutButton.Icon = new SymbolIcon(Symbol.Setting);

            var refreshMenu = new AppBarButton() { Label = "刷新" };
            appBar.SecondaryCommands.Add(refreshMenu);

            appBar.Visibility = Visibility.Visible;
            this.BottomAppBar = appBar;
            SetAppBarBackgroundColor();
        }

        public void SetAppBarBackgroundColor(double Opacity = 0.88)
        {
            if (this.BottomAppBar != null)
            {
                this.BottomAppBar.Opacity = Opacity;
                this.BottomAppBar.IsSticky = false;
                this.BottomAppBar.Foreground = new SolidColorBrush(Colors.White);
                
            }
        }
    }
}
