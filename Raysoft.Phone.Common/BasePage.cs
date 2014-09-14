using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Phone.UI.Input;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Raysoft.Phone.Common
{
    public class BasePage:Page
    {
        public BasePage()
        {
            StatusBar.GetForCurrentView().HideAsync();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// 进入页面事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            if (e.NavigationMode == NavigationMode.New)
                OnUpdateNavigationThemeTransition();
            
            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// 离开页面事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;

            base.OnNavigatedFrom(e);
        }

        /// <summary>
        /// 后退键点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            OnBackKeyPress(e);
        }


        protected virtual void OnBackKeyPress(BackPressedEventArgs e)
        {
            if (NavigationService.Frame != null && NavigationService.Frame.CanGoBack)
            {
                if (!e.Handled)
                {
                    e.Handled = true;
                    NavigationService.Frame.GoBack();
                }
            }
        }

        /// <summary>
        /// 更改当前页面的导航动画
        /// </summary>
        /// <param name="defaultTransition"></param>
        protected virtual void OnUpdateNavigationThemeTransition(bool defaultTransition = true)
        {
            if (defaultTransition)
            {
                this.Transitions = new TransitionCollection();
                this.Transitions.Add(new NavigationThemeTransition()
                {
                    DefaultNavigationTransitionInfo = new ContinuumNavigationTransitionInfo()
                });
            }
        }
    }
}
