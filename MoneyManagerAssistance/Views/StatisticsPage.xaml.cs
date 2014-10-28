using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍
using MoneyManagerAssistance.ViewModel;
using Raysoft.ModelLib;
using Raysoft.Phone.Common;
using Raysoft.Utility;
using Syncfusion.UI.Xaml.Charts;

namespace MoneyManagerAssistance.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class StatisticsPage : BasePage
    {
        
        PieChartViewModel viewModel;
        public StackingColumnChartViewModel ColumnViewModel { get; set; }
        private AccoutStatisticsViewModel vm;
        private AppBarButton addNewAccountings;

        public StatisticsPage()
        {
            this.InitializeComponent();
            vm = new AccoutStatisticsViewModel();
            this.PivotItem.DataContext = vm;
            this.PivotItem1.DataContext = vm;
            InitAppBar();
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //this.StatPivot.SelectedIndex = 0;
            var param = int.Parse(e.Parameter.ToString());
            if (param == 1)
            {
                this.StatPivot.Title = "支出统计图";
            }
            else if (param == 2)
            {
                this.StatPivot.Title = "收入统计图";
            }

            var conditionDic = new Dictionary<string, string>();
            conditionDic.Add("Account.AccountType",param.ToString());
            vm.SetStatisticsResult("SubCategoryId", conditionDic);
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

    public class CompanyDetail
    {
        public string CompanyName { get; set; }
        public double CompanyTurnover { get; set; }
    }

    public class model5
    {
        public string Expense { get; set; }
        public double Amount { get; set; }
    }

    public class PieChartViewModel
    {
        public PieChartViewModel()
        {
            CompanyDetails = new List<CompanyDetail>();
            CompanyDetails.Add(new CompanyDetail() { CompanyName = "工资收入", CompanyTurnover = 850 });
            CompanyDetails.Add(new CompanyDetail() { CompanyName = "证券收入", CompanyTurnover = 300 });
            CompanyDetails.Add(new CompanyDetail() { CompanyName = "意外收获", CompanyTurnover = 250 });
            CompanyDetails.Add(new CompanyDetail() { CompanyName = "彩票", CompanyTurnover = 450 });
            CompanyDetails.Add(new CompanyDetail() { CompanyName = "生意", CompanyTurnover = 700 });

            CompanyDetails1 = new List<CompanyDetail>();
            CompanyDetails1.Add(new CompanyDetail() { CompanyName = "Rolls Royce", CompanyTurnover = 75 });
            CompanyDetails1.Add(new CompanyDetail() { CompanyName = "Benz", CompanyTurnover = 50 });
            CompanyDetails1.Add(new CompanyDetail() { CompanyName = "Audi", CompanyTurnover = 45 });
            CompanyDetails1.Add(new CompanyDetail() { CompanyName = "BMW", CompanyTurnover = 70 });
            CompanyDetails1.Add(new CompanyDetail() { CompanyName = "Mahindra", CompanyTurnover = 35 });
            CompanyDetails1.Add(new CompanyDetail() { CompanyName = "Jaguar", CompanyTurnover = 65 });
            CompanyDetails1.Add(new CompanyDetail() { CompanyName = "Hero Honda", CompanyTurnover = 25 });

            this.Expenditure = new List<model5>();
            Expenditure.Add(new model5() { Expense = "E-Mail", Amount = 94658d });
            Expenditure.Add(new model5() { Expense = "Skype", Amount = 9090d });
            Expenditure.Add(new model5() { Expense = "Phone", Amount = 2577d });
            Expenditure.Add(new model5() { Expense = "Sms", Amount = 473d });
            Expenditure.Add(new model5() { Expense = "Twitter", Amount = 120d });

        }

        public IList<model5> Expenditure
        {
            get;
            set;
        }

        public IList<CompanyDetail> CompanyDetails
        {
            get;
            set;
        }
        public IList<CompanyDetail> CompanyDetails1
        {
            get;
            set;
        }
        private void ChartType_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
        }
    }

    public class Labelconvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is ChartAdornment)
            {
                ChartAdornment pieAdornment = value as ChartAdornment;
                if (pieAdornment.Item != null)
                    return String.Format(" 合计:  " + pieAdornment.YData + "元");
                else
                    return value;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

    public class Labelconvertor1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ChartAdornment pieAdornment = value as ChartAdornment;
            return String.Format((pieAdornment.Item as model5).Expense + " : " + pieAdornment.YData);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorConverter : IValueConverter
    {
        private SolidColorBrush ApplyLight(Color color)
        {
            return new SolidColorBrush(Color.FromArgb(color.A, (byte)(color.R * 0.9), (byte)(color.G * 0.9), (byte)(color.B * 0.9)));
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && (value is ChartAdornment))
            {
                ChartAdornment pieAdornment = value as ChartAdornment;
                int index = pieAdornment.Series.Adornments.IndexOf(pieAdornment);
                SolidColorBrush brush = pieAdornment.Series.ColorModel.GetBrush(index) as SolidColorBrush;
                return ApplyLight(brush.Color);
            }
            return value;
        }


        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

    public class StackingColumnChartViewModel
    {
        public StackingColumnChartViewModel()
        {
            this.MedalDetails = new ObservableCollection<Medal>();         
            MedalDetails.Add(new Medal() { CountryName = "USA", GoldMedals = 395, SilverMedals = 319, BronzeMedals = 296 });
            //MedalDetails.Add(new Medal() { CountryName = "Germany", GoldMedals = 247, SilverMedals = 284, BronzeMedals = 320 });
            MedalDetails.Add(new Medal() { CountryName = "Britain", GoldMedals = 207, SilverMedals = 255, BronzeMedals = 253 });
            MedalDetails.Add(new Medal() { CountryName = "France", GoldMedals = 191, SilverMedals = 212, BronzeMedals = 233 });
            MedalDetails.Add(new Medal() { CountryName = "Italy", GoldMedals = 190, SilverMedals = 157, BronzeMedals = 174 });          
            //MedalDetails.Add(new Medal() { CountryName = "Sweden", GoldMedals = 142, SilverMedals = 160, BronzeMedals = 173 });
            //MedalDetails.Add(new Medal() { CountryName = "Australia", GoldMedals = 131, SilverMedals = 137, BronzeMedals = 164 });
            MedalDetails.Add(new Medal() { CountryName = "Japan", GoldMedals = 123, SilverMedals = 112, BronzeMedals = 126 });
        }

        public ObservableCollection<Medal> MedalDetails { get; set; }
    }

    

    public class Medal
    {
        public string CountryName { get; set; }

        public double GoldMedals { get; set; }

        public double SilverMedals { get; set; }

        public double BronzeMedals { get; set; }

    }

    

}
