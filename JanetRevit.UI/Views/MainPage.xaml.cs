using Autodesk.Revit.UI;
using JanetRevit.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GlobalLowLevelHooks;
using JanetRevit.UI.ViewModels;

namespace JanetRevit.UI.Views
{
    public partial class MainPage : Page, IDockablePaneProvider
    {
        public MainPage(AddinDataProperties addinData)
        {
            InitializeComponent();
            DataContext = new MainPageViewModel(addinData);
            RegisterHooks();
        }
        
        private void RegisterHooks()
        {
            KeyboardHook hook = new KeyboardHook();
            hook.KeyDown += new KeyboardHook.KeyboardHookCallback(KeyUp);
            hook.Install();
        }

        private static void KeyUp(KeyboardHook.VKeys key)
        {
            MessageBox.Show("You have pressed " + key.ToString());
        }


        public void SetupDockablePane(DockablePaneProviderData dockablePaneData)
        {
            dockablePaneData.VisibleByDefault = false;
            dockablePaneData.FrameworkElement = this as FrameworkElement;
            dockablePaneData.InitialState = new DockablePaneState
            {
                DockPosition = DockPosition.Right,
            };
        }

        private void label_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(@"https://viatechnik.com/");
        }
    }
}
