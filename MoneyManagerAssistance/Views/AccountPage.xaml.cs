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
using Raysoft.ModelLib;
using Raysoft.Phone.Common;

namespace MoneyManagerAssistance.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AccountPage : BasePage
    {
        private AccountTypeSelector selector;
        
        
        public AccountPage()
        {
            this.InitializeComponent();
            this.AccoutTypeInput.AddHandler(TappedEvent, new TappedEventHandler(AccoutTypeInput_OnTapped), true);
            
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        protected override void OnBackKeyPress(BackPressedEventArgs e)
        {
            if (this.selector != null && this.layout.Children.Contains(selector))
            {
                HideStoryboardPlay();
                e.Handled = true;
            }
            else
            {
                base.OnBackKeyPress(e);
            }
        }


        private void AccoutTypeInput_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            StoryboardPlay();
        }

        private void StoryboardPlay()
        {
            if (selector != null && this.layout.Children.Contains(selector))
            {
                return;
            }

            DoubleAnimation animation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromMilliseconds(300),
                From = Window.Current.Bounds.Width,
                To = Window.Current.Bounds.Width - 210
            };

            selector = new AccountTypeSelector();
            selector.RenderTransform = new CompositeTransform();
            selector.SelectTappedEvent += (sender, args) =>
            {
                var item = args;
                this.AccoutTypeInput.Text = args;
            };
            

            Storyboard.SetTarget(animation,selector);
            Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)");

            Storyboard sb  = new Storyboard();
            sb.Children.Add(animation);
            this.layout.Children.Add(selector);
            Grid.SetRow(selector,0);
            Grid.SetRowSpan(selector,3);
            sb.Completed += (sender, o) =>
            {
                
            };
            sb.Begin();
        }

        private void HideStoryboardPlay()
        {
            if (selector == null || !this.layout.Children.Contains(selector))
            {
                return;
            }

            DoubleAnimation animation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromMilliseconds(300),
                To = Window.Current.Bounds.Width
            };

            Storyboard.SetTarget(animation, selector);
            Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)");

            Storyboard sb = new Storyboard();
            sb.Children.Add(animation);
            sb.Completed += (sender, o) => this.layout.Children.Remove(selector);
            
            sb.Begin();
        }

        private void Layout_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            if (selector != null)
            {
                var position = e.GetPosition(sender as Grid);
                if (position.X < Window.Current.Bounds.Width - 210)
                {
                    HideStoryboardPlay();
                    selector.CollapsedOutView();
                }
            }
        }

        private async void SaveAppBarButton_OnClick(object sender, RoutedEventArgs e)
        {
            var accountRec = new Accout()
            {
                AccountDate = ((DateTime)DP1.Value).Date.ToString("yyyy-MM-dd HH:mm:ss"),
                MemberId = 1,
                AccountType = 1,
                SubCategoryId = 1,
                AccountSum = int.Parse(this.AccountSumTextBox.Text),
                AccountSourceType = 1,
                Description = this.AccountDesTextBox.Text,
                ABookId = 1,
            };

            //await viewModel.SaveAccountRecord(accountRec);
        }


        private async void SaveAndBackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //await viewModel.GetAllAccountRecords();
        }
    }
}
