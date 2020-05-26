using OnePomodoro.Helpers;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace OnePomodoro.PomodoroViews
{
    [Title("TheFirst")]
    [Screenshot("/Assets/Screenshots/TheFirst.png")]
    [FunctionTags(Tags.ImplicitAnimation)]
    [SourceCode("https://github.com/DinoChan/OnePomodoro/blob/master/OnePomodoro/OnePomodoro/PomodoroViews/TheFirst.xaml.cs")]
    public sealed partial class TheFirst : PomodoroView
    {
        public TheFirst()
        {
            InitializeComponent();
        }



        //private void OnOptionsClick(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        //{
        //    RaiseNavigateToOptionsPage();
        //}

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    base.OnNavigatedTo(e);
        //    Window.Current.SetTitleBar(null);
        //}
    }
}
