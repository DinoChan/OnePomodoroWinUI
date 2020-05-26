﻿using OnePomodoro.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

using Microsoft.Toolkit.Uwp.UI.Animations;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using Windows.UI;
using System.Numerics;
using Windows.UI.Composition;
using System.Threading.Tasks;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace OnePomodoro.PomodoroViews
{
    [Title("Smple Text With Acrylic")]
    [Screenshot("/Assets/Screenshots/TheBigOne.png")]
    public sealed partial class SmpleTextWithAcrylic : PomodoroView
    {
        private PointLight _inworkPointLight;
        private PointLight _breakPointLight;

        public SmpleTextWithAcrylic()
        {
            this.InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            await LayoutRoot.Fade(0.0f, duration: 0, delay: 0, easingMode: Microsoft.UI.Xaml.Media.Animation.EasingMode.EaseIn).StartAsync();
            await LayoutRoot.Fade(1.0f, duration: 1500, delay: 0).StartAsync();
            ShowTextShimming();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            StopTextShimming();
        }



        private void ShowTextShimming()
        {
            if (_inworkPointLight != null)
                return;

            var compositor = Window.Current.Compositor;
            if (_inworkPointLight == null)
            {
                var titleVisual = VisualExtensions.GetVisual(InworkShadow);
                _inworkPointLight = compositor.CreatePointLight();

                _inworkPointLight.Color = Color.FromArgb(255, 217, 17, 83);
                _inworkPointLight.CoordinateSpace = titleVisual;
                _inworkPointLight.Targets.Add(titleVisual);
                _inworkPointLight.Offset = new Vector3(-(float)PomodoroPanel.ActualWidth * 3, (float)PomodoroPanel.ActualHeight / 2, 300.0f);
            }
            var offsetAnimation = compositor.CreateScalarKeyFrameAnimation();
            offsetAnimation.InsertKeyFrame(1.0f, (float)PomodoroPanel.ActualWidth * 3, compositor.CreateLinearEasingFunction());
            offsetAnimation.Duration = TimeSpan.FromMilliseconds(10000);
            offsetAnimation.IterationBehavior = AnimationIterationBehavior.Forever;

            _inworkPointLight.StartAnimation("Offset.X", offsetAnimation);



            if (_breakPointLight == null)
            {
                var titleVisual = VisualExtensions.GetVisual(BreakShadow);
                _breakPointLight = compositor.CreatePointLight();

                _breakPointLight.Color = Color.FromArgb(255, 217, 17, 83);
                _breakPointLight.CoordinateSpace = titleVisual;
                _breakPointLight.Targets.Add(titleVisual);
                _breakPointLight.Offset = new Vector3(-(float)PomodoroPanel.ActualWidth * 3, (float)PomodoroPanel.ActualHeight / 2, 300.0f);
            }
            offsetAnimation = compositor.CreateScalarKeyFrameAnimation();
            offsetAnimation.InsertKeyFrame(1.0f, (float)PomodoroPanel.ActualWidth * 3, compositor.CreateLinearEasingFunction());
            offsetAnimation.Duration = TimeSpan.FromMilliseconds(10000);
            offsetAnimation.IterationBehavior = AnimationIterationBehavior.Forever;

            _breakPointLight.StartAnimation("Offset.X", offsetAnimation);
        }

        private void StopTextShimming()
        {
            //_inworkPointLight?.StopAnimation("Offset.X");
            //_breakPointLight?.StopAnimation("Offset.X");
        }
    }
}
