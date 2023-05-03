using Autodesk.Revit.UI;
using JanetRevit.Properties;
using JanetRevit.UI.RevitUI;
using System;
using System.Windows;

namespace JanetRevit
{
    public class SetupInterface
    {
        public SetupInterface()
        {

        }
        public void Initialize(UIControlledApplication app)
        {
            try
            {
                var managerCommandsPanel = app.CreateRibbonPanel(Resource.RevitPanelName);

                var familyManagerShowButtonData = new RevitPushButtonDataModel
                {
                    Label = Resource.RevitButtonName,
                    Panel = managerCommandsPanel,
                    Tooltip = Resource.RevitButtonTooltip,
                    CommandNamespacePath = CatchKeyboardShortcutCommand.GetPath(),
                    AssemblyLocation = CatchKeyboardShortcutCommand.GetAssemblyLocation(),
                    IconImageName = "janet_icon.png",
                    TooltipImageName = "janet_icon.png"
                };
                RevitPushButton.Create(familyManagerShowButtonData);

                var scriptEdit = new RevitPushButtonDataModel
                {
                    Label = Resource.ScriptEditButtonName,
                    Panel = managerCommandsPanel,
                    Tooltip = Resource.ScriptEditButtonTooltip,
                    CommandNamespacePath = ScriptEditCommand.GetPath(),
                    AssemblyLocation = ScriptEditCommand.GetAssemblyLocation(),
                    IconImageName = "script_edit_icon.png",
                    TooltipImageName = "script_edit_icon.png"
                };
                RevitPushButton.Create(scriptEdit);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + e.StackTrace);
            }

        }

    }
}
