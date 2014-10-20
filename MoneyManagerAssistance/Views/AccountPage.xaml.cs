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
    public sealed partial class AccountPage : BasePage
    {
        private AccountCategorySelector selector;
        private AccountViewModel vm;
        
        
        public AccountPage()
        {
            this.InitializeComponent();
            vm = new AccountViewModel();
            this.DataContext = vm;
            
            this.AccoutCategoryInput.AddHandler(TappedEvent, new TappedEventHandler(AccoutTypeInput_OnTapped), true);
            this.Loaded += OnLoaded;
            
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            this.MemberComboBox.SelectedIndex = 0;
            this.AcntTypeComboBox.SelectedIndex = 0;
            this.AccountSrcComboBox.SelectedIndex = 0;
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

            if (selector == null)
            {
                selector = new AccountCategorySelector();
                selector.BigAccountTypeIndex = this.AcntTypeComboBox.SelectedIndex + 1;
                selector.RenderTransform = new CompositeTransform();
                selector.SelectTappedEvent += (sender, args) =>
                {
                    this.AccoutCategoryInput.Text = args.Name;
                    this.AccoutCategoryInput.Tag = args.Id;
                };
            }
            else
            {
                selector.BigAccountTypeIndex = this.AcntTypeComboBox.SelectedIndex + 1;
            }

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

        private void SaveAppBarButton_OnClick(object sender, RoutedEventArgs e)
        {
            var accountRec = new Account()
            {
                AccountDate = (DateTime)DP1.Value,
                MemberId = this.MemberComboBox.SelectedIndex + 1,
                AccountType = this.AcntTypeComboBox.SelectedIndex + 1,
                SubCategoryId = int.Parse(this.AccoutCategoryInput.Tag.ToString()),
                AccountSum = float.Parse(this.AccountSumTextBox.Text),
                AccountSourceId = this.AccountSrcComboBox.SelectedIndex + 1,
                Description = this.AccountDesTextBox.Text,
                ABookId = 1,
            };

            var result = vm.SaveAccountRecord(accountRec);
        }


        private async void SaveAndBackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //await viewModel.GetAllAccountRecords();
        }
    }
}
