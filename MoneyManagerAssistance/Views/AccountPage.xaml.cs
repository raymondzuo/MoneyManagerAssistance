using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
using Raysoft.Phone.Common;

namespace MoneyManagerAssistance.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AccountPage : BasePage,INotifyPropertyChanged
    {
        public AccountPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
            this.AccoutTypeInput.AddHandler(TappedEvent, new TappedEventHandler(AccoutTypeInput_OnTapped), true);
            this.Loaded += (sender, args) =>
            {
                var width = Window.Current.Bounds.Width;
                //this.AccountTypeSelector.Margin = new Thickness(width - 50, 0, 0, 0);
            };

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

        private String dateformatString = "M/d/yyyy";

        public String DateFormat
        {
            get { return dateformatString; }
            set
            {
                dateformatString = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void AccoutTypeInput_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            StoryboardPlay();
        }

        private void StoryboardPlay()
        {
            DoubleAnimation animation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromMilliseconds(300),
                From = 480,
                To = 200
            };

            var selector = new AccountTypeSelector();
            selector.RenderTransform = new CompositeTransform();

            Storyboard.SetTarget(animation,selector);
            Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)");

            Storyboard sb  = new Storyboard();
            sb.Children.Add(animation);
            this.layout.Children.Add(selector);
            Grid.SetRow(selector,2);
            sb.Completed += (sender, o) =>
            {
                
            };
            sb.Begin();
        }
    }
}
