using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.Exceptions;
using System.Reflection;

namespace JanetRevit.UI.RevitUI
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class MainDockablePaneShowCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var dpid = new DockablePaneId(DockablePaneIdentifiers.GetPaneIdentifier());
            var dp = commandData.Application.GetDockablePane(dpid);
            dp.Show();

            return Result.Succeeded;
        }

        public static string GetPath()
        {
            return typeof(MainDockablePaneShowCommand).Namespace + "." + nameof(MainDockablePaneShowCommand);
        }

        public static string GetAssemblyLocation() =>
            Assembly.GetExecutingAssembly().Location;
    }
}
