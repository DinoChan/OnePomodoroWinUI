using OnePomodoro.ViewModels;
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

namespace OnePomodoro.Views
{
    public sealed partial class GeneralSettingsView : UserControl
    {
        public GeneralSettingsView()
        {
            this.InitializeComponent();
            Loaded += GeneralSettingsView_Loaded;
        }

        private void GeneralSettingsView_Loaded(object sender, RoutedEventArgs e)
        {
          
        }

        private GeneralSettingsViewModel ViewModel => DataContext as GeneralSettingsViewModel;

        private void OnDefaultPomodoroLengthClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Settings.PomodoroLength = 25;
        }

        private void OnDefaultShortBreakLengthClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Settings.ShortBreakLength = 5;
        }

        private void OnDefaultLongBreakLengthClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Settings.LongBreakLength= 15;
        }

        private void OnDefaultLongBreakAfterClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Settings.LongBreakAfter = 4;
        }
    }
}
