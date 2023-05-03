using System.Windows.Input;

namespace JanetRevit.UI.ViewModels
{
    public interface BasePageViewModel
    {
        ICommand OnLoaded { get; set; }
    }
}
