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

namespace MoneyManagerAssistance.SubViews
{
    public sealed partial class AccountTypeSelector : UserControl
    {
        private ObservableCollection<TypeInfo> TypeGroups;
        public AccountTypeSelector()
        {
            this.InitializeComponent();
            
            this.Loaded += OnLoaded;

            TypeGroups = new ObservableCollection<TypeInfo>();
            TypeGroups.Add(new TypeInfo() { MainType = "交通消费", SubType = new List<string>() { "公交消费", "地铁消费", "汽车消费" } });
            TypeGroups.Add(new TypeInfo() { MainType = "衣服消费", SubType = new List<string>() { "买衣服", "买鞋", "买裤子" } });
            TypeGroups.Add(new TypeInfo() { MainType = "餐饮", SubType = new List<string>() { "餐馆消费", "啤酒", "饮料" } });

            (this.Resources["ViewSource"] as CollectionViewSource).Source = TypeGroups;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            
            
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            var lv = sender as ListView;
            
            lv.ItemsSource = (this.Resources["ViewSource"] as CollectionViewSource).View.CollectionGroups;
        }

        //internal List<TypeGroup<object>> GetGroupsByCategory()
        //{
        //    List<TypeGroup<object>> groups = new List<TypeGroup<object>>();

        //    var query = from item in Collection
        //                orderby ((Item)item).Category
        //                group item by ((Item)item).Category into g
        //                select new { GroupName = g.Key, Items = g };
        //    foreach (var g in query)
        //    {
        //        GroupInfoList<object> info = new GroupInfoList<object>();
        //        info.Key = g.GroupName;
        //        foreach (var item in g.Items)
        //        {
        //            info.Add(item);
        //        }
        //        groups.Add(info);
        //    }

        //    return groups;

        //}
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
