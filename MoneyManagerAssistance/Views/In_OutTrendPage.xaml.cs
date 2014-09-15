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
using Raysoft.Phone.Common;

namespace MoneyManagerAssistance.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class In_OutTrendPage : BasePage
    {
        public In_OutTrendPage()
        {
            this.InitializeComponent();
            
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.DataContext = new LineViewModel();
        }
    }

    public class LineViewModel
    {
        public LineViewModel()
        {
            this.power = new ObservableCollection<Entertainment>();
            DateTime yr = new DateTime(2002, 5, 1);
            power.Add(new Entertainment() { Year = yr.AddYears(1), Sports = 28, Books = 31, Music = 36 });
            power.Add(new Entertainment() { Year = yr.AddYears(2), Sports = 24, Books = 24, Music = 32 });
            power.Add(new Entertainment() { Year = yr.AddYears(3), Sports = 26, Books = 30, Music = 34 });
            power.Add(new Entertainment() { Year = yr.AddYears(4), Sports = 27, Books = 36, Music = 41 });
            power.Add(new Entertainment() { Year = yr.AddYears(5), Sports = 32, Books = 36, Music = 42 });
            power.Add(new Entertainment() { Year = yr.AddYears(6), Sports = 35, Books = 39, Music = 42 });
            power.Add(new Entertainment() { Year = yr.AddYears(7), Sports = 30, Books = 37, Music = 43 });

        }


        public ObservableCollection<Entertainment> power
        {
            get;
            set;
        }

    }
    public class Entertainment
    {
        public DateTime Year { get; set; }

        public double Sports { get; set; }
        public double Books { get; set; }
        public double Music { get; set; }
        public double Dance { get; set; }
    }
}
