using System.Windows.Input;
using JanetRevit.Core.Models;

namespace JanetRevit.UI.ViewModels
{
    public class BlankPageViewModel : BaseViewModel, BasePageViewModel
    {
        public BlankPageViewModel(AddinDataProperties addinDataProperties) : base(addinDataProperties)
        {
        }

        public ICommand OnLoaded { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}
