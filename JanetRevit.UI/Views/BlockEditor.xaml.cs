using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using JanetRevit.UI.ViewModels;
using RevitSwitchAddin;

namespace JanetRevit.UI.Views
{
    public partial class BlockEditor : Window
    {
        public BlockEditor()
        {
            InitializeComponent();
            ((BlockEditorViewModel) DataContext).CodeChanged += ChangeCode;
            textEditor.ShowLineNumbers = true;
        }

        private void ChangeCode(object sender, CodeChangedEventArgs e)
        {
            Stream textStream = GenerateStreamFromString(e.Code);
            textEditor.Load(textStream);
        }
        
        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private void CloseWindowEvent(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BlockEditor_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                    this.DragMove(); 
        }

        private void SaveCode(object sender, RoutedEventArgs e)
        {
            ((BlockEditorViewModel) DataContext).SaveCode(textEditor.Text);
        }
    }
}