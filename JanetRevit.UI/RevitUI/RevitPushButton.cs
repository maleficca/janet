using System;
using System.Windows.Media.Imaging;
using System.Reflection;
using Autodesk.Revit.UI;

namespace JanetRevit.UI.RevitUI
{
    public static class RevitPushButton
    {
        public static PushButton Create(RevitPushButtonDataModel data)
        {
            var btnDataName = Guid.NewGuid().ToString();
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            BitmapImage largeImage = new BitmapImage(new Uri($"pack://application:,,,/{assemblyName};component/Resources/Icons/{data.IconImageName}"));
            BitmapImage tooltipImage = new BitmapImage(new Uri($"pack://application:,,,/{assemblyName};component/Resources/Icons/{data.TooltipImageName}"));

            var btnData = new PushButtonData(btnDataName, data.Label, data.AssemblyLocation, data.CommandNamespacePath)
            {
                ToolTip = data.Tooltip,
                LargeImage = largeImage,
                ToolTipImage = tooltipImage
            };

            return data.Panel.AddItem(btnData) as PushButton;
        }
    }
}
