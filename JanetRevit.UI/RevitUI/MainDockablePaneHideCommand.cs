using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;

namespace JanetRevit.UI.RevitUI
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class MainDockablePaneHideCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var dpid = new DockablePaneId(DockablePaneIdentifiers.GetPaneIdentifier());
            var dp = commandData.Application.GetDockablePane(dpid);
            dp.Hide();

            return Result.Succeeded;
        }

        public static string GetPath()
        {
            return typeof(MainDockablePaneHideCommand).Namespace + "." + nameof(MainDockablePaneHideCommand);
        }
    }
}
