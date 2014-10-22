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
using System.ComponentModel;

namespace MoneyManagerAssistance.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CategoryPage : BasePage
    {
        private CategoryDataViewModel vm;
        public CategoryPage()
        {
            this.InitializeComponent();
            this.Loaded += OnLoaded;
            vm = new CategoryDataViewModel();
            this.MainGrid.DataContext = vm;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            //this.MainGrid.DataContext = new CategoryDataViewModel();
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
    }

    public class CategoryDataViewModel
    {
        public CategoryDataViewModel()
        {
            CategoricalDatas = new ObservableCollection<CategoryData>();
            CategoricalDatas.Add(new CategoryData("Metal", 5));
            CategoricalDatas.Add(new CategoryData("Plastic", 10));
            CategoricalDatas.Add(new CategoryData("Silver", 15));
            CategoricalDatas.Add(new CategoryData("Iron", 20));

        }
        public ObservableCollection<CategoryData> CategoricalDatas
        {
            get;
            set;
        }
    }

    public class CategoryData : INotifyPropertyChanged
    {
        private string category; private double value;

        public CategoryData(string category, double value)
        {
            Category = category; Value = value;
        }

        public string Category
        {
            get
            { return category; }
            set
            {
                if (category != value)
                {
                    category = value;
                    OnPropertyChanged("Category");
                }
            }
        }

        public double Value
        {
            get
            { return value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    OnPropertyChanged("Value");
                }
            }
        }


        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
