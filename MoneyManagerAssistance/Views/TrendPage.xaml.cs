using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍
using MoneyManagerAssistance.Annotations;
using Raysoft.Phone.Common;
using Raysoft.Utility;
using Syncfusion.UI.Xaml.Charts;

namespace MoneyManagerAssistance.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TrendPage : BasePage
    {
        DispatcherTimer delayLoader = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(1) };
        private AppBarButton addNewAccountings;
        public TrendPage()
        {
            this.InitializeComponent();
            this.Loaded += (o, args) => delayLoader.Start();
            InitAppBar();
            delayLoader.Tick += timer_Tick;
            this.lineChart.Opacity = 0;
        }

        void timer_Tick(object sender, object e)
        {
            delayLoader.Tick -= timer_Tick;
            delayLoader.Stop();
            lineChart.Opacity = 1;
            
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
            //this.lineChart.Legend
            
        }

        private void InitAppBar()
        {
            var appBar = new CommandBar();

            addNewAccountings = new AppBarButton() { Label = "月视图" };
            addNewAccountings.Icon = new BitmapIcon() { UriSource = new Uri("ms-appx:///Assets/AppBarIcon/Add-New.png", UriKind.RelativeOrAbsolute) };
            addNewAccountings.Click += (sender, args) =>
            {
                var vm = this.DataContext as LineViewModel;
                vm.Format = "yyyy-m";
                vm.LoadMore();

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

    public class LineViewModel:INotifyPropertyChanged
    {
        public LineViewModel()
        {
            this.power = new ObservableCollection<Entertainment>();
            DateTime yr = new DateTime(2002, 5, 1);
            power.Add(new Entertainment() { Year = yr.AddYears(1), Sports = 28, Books = 31, Music = 36,Type = "收入"});
            power.Add(new Entertainment() { Year = yr.AddYears(2), Sports = 24, Books = 24, Music = 32, Type = "收入" });
            power.Add(new Entertainment() { Year = yr.AddYears(3), Sports = 26, Books = 30, Music = 34, Type = "收入" });
            power.Add(new Entertainment() { Year = yr.AddYears(4), Sports = 27, Books = 36, Music = 41, Type = "收入" });
            power.Add(new Entertainment() { Year = yr.AddYears(5), Sports = 32, Books = 36, Music = 42, Type = "支出" });
            power.Add(new Entertainment() { Year = yr.AddYears(6), Sports = 35, Books = 39, Music = 42, Type = "支出" });
            power.Add(new Entertainment() { Year = yr.AddYears(7), Sports = 30, Books = 37, Music = 43, Type = "支出" });

            Format = "yyyy";
            type = new ObservableCollection<LegendItem>();
            var item1 = new LegendItem();
            item1.Label = "收入";

            var item2 = new LegendItem();
            item2.Label = "收入";

            type.Add(item1);
            type.Add(item2);
            type.Add(item2);

        }

        public void LoadMore()
        {
            DateTime yr = new DateTime(2002, 5, 1);
            power.Add(new Entertainment() { Year = yr.AddYears(8), Sports = 30, Books = 37, Music = 43, Type = "支出" });
        }


        public ObservableCollection<Entertainment> power
        {
            get;
            set;
        }

        public ObservableCollection<LegendItem> type
        {
            get;
            set;
        }

        private string _format;

        public string Format
        {
            set
            {
                this._format = value;
                this.OnPropertyChanged();
            }
            get { return this._format; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class Entertainment
    {
        public DateTime Year { get; set; }

        public double Sports { get; set; }
        public double Books { get; set; }
        public double Music { get; set; }
        public double Dance { get; set; }

        public string Type { get; set; }
    }

    public class AccoutType
    {
        public string Type { get; set; }
    }
}
