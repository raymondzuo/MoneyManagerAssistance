using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供
using MoneyManagerAssistance.ViewModel;
using Raysoft.ModelLib;

namespace MoneyManagerAssistance.SubViews
{
    public sealed partial class AccountCategorySelector : UserControl
    {
        private ObservableCollection<TypeInfo> TypeGroups;
        public EventHandler<SubAccountCategory> SelectTappedEvent;
        public AccountCateSelectViewModel vm;
        public AccountCategorySelector()
        {
            this.InitializeComponent();
            vm = new AccountCateSelectViewModel();
            this.DataContext = vm;
            this.Loaded += OnLoaded;

            //TypeGroups = new ObservableCollection<TypeInfo>();
            //TypeGroups.Add(new TypeInfo() { MainType = "交通消费", SubType = new List<string>() { "公交消费", "地铁消费", "汽车消费" } });
            //TypeGroups.Add(new TypeInfo() { MainType = "衣服消费", SubType = new List<string>() { "买衣服", "买鞋", "买裤子" } });
            //TypeGroups.Add(new TypeInfo() { MainType = "餐饮", SubType = new List<string>() { "餐馆消费", "啤酒", "饮料" } });

            //(this.Resources["ViewSource"] as CollectionViewSource).Source = vm.AccountCategoryForBindings;
        }

        #region DependencyProperty

        public int? BigAccountTypeIndex {
            get { return (int?)GetValue(BigAccountTypeIndexProperty); }
            set { SetValue(BigAccountTypeIndexProperty,value);}
        }

        public static readonly DependencyProperty BigAccountTypeIndexProperty = DependencyProperty.Register("BigAccountTypeIndex", typeof(int?), typeof(AccountCategorySelector), new PropertyMetadata(null, OnPropertyChanged));

        public static void OnPropertyChanged(DependencyObject sender,DependencyPropertyChangedEventArgs e)
        {
            var selector = sender as AccountCategorySelector;
            if(selector == null)
                return;

            selector.vm.SetAccountCategorys((int)e.NewValue);
            (selector.Resources["ViewSource"] as CollectionViewSource).Source = selector.vm.AccountCategoryForBindings;
        }

        #endregion

        public void CollapsedOutView()
        {
            this.SemanticZoom.IsZoomedInViewActive = true;
            
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            
            
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            var lv = sender as ListView;
            
            lv.ItemsSource = (this.Resources["ViewSource"] as CollectionViewSource).View.CollectionGroups;
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = sender as ListView;
            if(listView.SelectedItem == null)
                return;

            var item = listView.SelectedItem as SubAccountCategory;
            if (SelectTappedEvent != null)
            {
                SelectTappedEvent(sender, item);
            }
        }
    }



    public class TypeInfo
    {
        public string MainType { get; set; }
        public List<string> SubType { get; set; }
    }

    public class TypeGroup<T> : List<Object>
    {
        public object Key { get; set; }

        public new IEnumerator<object> GetEnumerator()
        {
            return base.GetEnumerator();
        }
    }
}
