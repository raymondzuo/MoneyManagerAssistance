using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Raysoft.Phone.Common
{
    public class NavigationService
    {
        public static Frame Frame
        {
            get { return Window.Current.Content as Frame; }
        }

        /// <summary>
        /// 导航至某页面
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async static Task<bool> Navigate(Type type)
        {
            if (Frame != null)
            {
                return Frame.Navigate(type);
            }

            return false;
        }

        /// <summary>
        /// 导航某页面
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async static Task<bool> Navigate(Type type, object parameter)
        {
            if (Frame != null)
            {
                return Frame.Navigate(type, parameter);
            }

            return false;
        }

        /// <summary>
        /// 回退
        /// </summary>
        public static void GoBack()
        {
            if (Frame != null)
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
            }
        }

        /// <summary>
        /// 是否可以后腿
        /// </summary>
        /// <returns></returns>
        public static bool CanGoBack()
        {
            if (Frame != null)
            {
                return Frame.CanGoBack;
            }

            return false;
        }

        public static bool RemoveLastPage()
        {
            if (Frame != null)
            {
                var page = (Frame.Content as Page);
                if (page != null)
                {
                    Frame.BackStack.RemoveAt(Frame.BackStack.Count - 1);
                    return true;
                }
            }

            return false;
        }
    }
}
