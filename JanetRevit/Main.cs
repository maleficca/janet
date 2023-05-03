using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using GlobalLowLevelHooks;
using JanetRevit.UI.RevitUI;

namespace JanetRevit
{
    public class Main : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            // Initialize whole plugin's user interface.
            var ui = new SetupInterface();
            ui.Initialize(application);

            var familyManagerRegisterCommand = new RegisterMainDockablePaneCommand();
            familyManagerRegisterCommand.Execute(application);
            
            return Result.Succeeded;
        }
        
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}