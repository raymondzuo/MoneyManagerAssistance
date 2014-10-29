using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供
using MoneyManagerAssistance.ViewModel;

namespace MoneyManagerAssistance.SubViews
{
    public sealed partial class MoneyGlance : UserControl
    {
        private bool isExpand = false;
        /// <summary>
        /// 4个参数分别是tag名，箭头图片，收缩的grid，是否展开
        /// </summary>
        private List<Tuple<string, UIElement, UIElement,bool>> AnimationInfos = new List<Tuple<string, UIElement, UIElement, bool>>();

        private MainPageViewModel vm;

        public MoneyGlance()
        {
            this.InitializeComponent();
            vm = MainPageViewModel.Instance;
            this.DataContext = vm;
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var inDic = new Dictionary<string, string>();
            inDic.Add("Account.AccountType","2");
            vm.SetInAccountResult("DetailAll", inDic);
            inDic.Clear();
            inDic.Add("Account.AccountType", "1");
            vm.SetOutAccountResult("DetailAll", inDic);
        }

        private void Image_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var img = sender as Image;
            var tag = img.Tag.ToString();
            Grid animationGrid = null;
            var animationInfo = AnimationInfos.FirstOrDefault(p => p.Item1.Equals(tag));

            foreach (var child in this.RootGrid.Children)
            {
                var grid = child as Grid;
                if (grid != null&&grid.Name.Equals(tag))
                {
                    animationGrid = grid;
                    animationGrid.Visibility = Visibility.Visible;
                    break;
                }
            }

            if (animationInfo == null || !animationInfo.Item4)
            {
                //如果需要显示一个展开页面，将此注释打开。
                //if (previousAnimationElements != null)
                //{
                //    PlayUpAnimation(previousAnimationElements.Item1, previousAnimationElements.Item2);
                //    previousAnimationElements = null;
                //}

                PlayDownAnimation(img, animationGrid);
                if (AnimationInfos.FirstOrDefault(p => p.Item1.Equals(tag)) == null)
                {
                    AnimationInfos.Add(new Tuple<string, UIElement, UIElement, bool>(tag,img,animationGrid,true));
                }
                else
                {
                    var itsAnimationInfo = AnimationInfos.FirstOrDefault(p => p.Item1.Equals(tag));
                    AnimationInfos.Remove(itsAnimationInfo);
                    AnimationInfos.Add(new Tuple<string, UIElement, UIElement, bool>(tag, img, animationGrid, true));
                }
            }
            else
            {
                PlayUpAnimation(img, animationGrid);
                if (AnimationInfos.FirstOrDefault(p => p.Item1.Equals(tag)) != null)
                {
                    var itsAnimationInfo = AnimationInfos.FirstOrDefault(p => p.Item1.Equals(tag));
                    AnimationInfos.Remove(itsAnimationInfo);
                    AnimationInfos.Add(new Tuple<string, UIElement, UIElement, bool>(tag, img, animationGrid, false));
                } 
            }

        }

        private void PlayDownAnimation(UIElement rotationElement, UIElement dropDownElement)
        {
            DoubleAnimation rotationAnimation = new DoubleAnimation()
            {
                From = 0,
                To = 180,
                Duration = TimeSpan.FromMilliseconds(300)
            };

            DoubleAnimation dropDownAnimation = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(300)
            };

            Storyboard.SetTarget(rotationAnimation,rotationElement);
            Storyboard.SetTargetProperty(rotationAnimation, "(UIElement.RenderTransform).(CompositeTransform.Rotation)");

            Storyboard.SetTarget(dropDownAnimation, dropDownElement);
            Storyboard.SetTargetProperty(dropDownAnimation, "(UIElement.RenderTransform).(CompositeTransform.ScaleY)");

            Storyboard sb = new Storyboard();
            sb.Children.Add(rotationAnimation);
            sb.Children.Add(dropDownAnimation);
            sb.Completed += (sender, o) =>
            {
                this.isExpand = true;
                
            };
            sb.Begin();
        }

        private void PlayUpAnimation(UIElement rotationElement, UIElement dropDownElement)
        {
            DoubleAnimation rotationAnimation = new DoubleAnimation()
            {
                From = 180,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(300)
            };

            DoubleAnimation dropDownAnimation = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(300)
            };

            Storyboard.SetTarget(rotationAnimation, rotationElement);
            Storyboard.SetTargetProperty(rotationAnimation, "(UIElement.RenderTransform).(CompositeTransform.Rotation)");

            Storyboard.SetTarget(dropDownAnimation, dropDownElement);
            Storyboard.SetTargetProperty(dropDownAnimation, "(UIElement.RenderTransform).(CompositeTransform.ScaleY)");

            Storyboard sb = new Storyboard();
            sb.Children.Add(rotationAnimation);
            sb.Children.Add(dropDownAnimation);
            sb.Completed += (sender, o) =>
            {
                dropDownElement.Visibility = Visibility.Collapsed;
                this.isExpand = false;
            };
            sb.Begin();
        }
    }
}
