using System.Windows;
using JanetRevit.UI.Views;

namespace WPFTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            BlockEditor window = new BlockEditor();
            window.Show();
        }
    }
}