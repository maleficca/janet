using System;
using System.Windows;
using GlobalLowLevelHooks;

namespace JanetRevit.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class JanetWindow
    {
        public KeyboardHook hook;
        public event EventHandler KeyPressedHandler;
        public JanetWindow()
        {
            InitializeComponent();
            RegisterHooks();
        }
        
        private void RegisterHooks()
        {
            hook = new KeyboardHook();
            hook.KeyDown += new KeyboardHook.KeyboardHookCallback(KeyUp);
            hook.Install();
        }

        private void KeyUp(KeyboardHook.VKeys key)
        {
            hook.Uninstall();
            KeyPressedHandler?.Invoke(this, new KeyPressedEventArgs(key.ToString()));
        }
    }
    
    public class KeyPressedEventArgs: EventArgs
    {
        public string PressedKey;

        public KeyPressedEventArgs(string key)
        {
            PressedKey = key;
        }
    }
}