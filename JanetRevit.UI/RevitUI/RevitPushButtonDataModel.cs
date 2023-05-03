using Autodesk.Revit.UI;

namespace JanetRevit.UI.RevitUI
{
    public class RevitPushButtonDataModel
    {
        public string Label { get; set; }
        public RibbonPanel Panel { get; set; }
        public string CommandNamespacePath { get; set; }
        public string Tooltip { get; set; }
        public string IconImageName { get; set; }
        public string TooltipImageName { get; set; }
        public string AssemblyLocation { get; set; }
        public RevitPushButtonDataModel()
        {

        }
    }
}
