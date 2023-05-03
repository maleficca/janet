using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using JanetRevit.UI.Views;
using System.Reflection;

namespace JanetRevit.UI.RevitUI
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class ScriptEditCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            BlockEditor editor = new BlockEditor();
            editor.Show();
            return Result.Succeeded;
        }

        public static string GetPath()
        {
            return typeof(ScriptEditCommand).Namespace + "." + nameof(ScriptEditCommand);
        }

        public static string GetAssemblyLocation() => Assembly.GetExecutingAssembly().Location;
    }
}