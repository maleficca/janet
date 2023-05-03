using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Threading;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using JanetRevit.Core.Handlers;
using JanetRevit.Core.Helpers;
using JanetRevit.Core.Models;
using Newtonsoft.Json;
using RevitSwitchAddin;

namespace JanetRevit.UI.RevitUI
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class CatchKeyboardShortcutCommand: IExternalCommand
    {
        private JanetWindow window;
        private ExternalCommandData _commandData;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            _commandData = commandData;
            window = new JanetWindow();
            window.KeyPressedHandler += WindowOnKeyPressedHandler;
            window.ShowDialog();
            return Result.Succeeded;
            
        }


        private void WindowOnKeyPressedHandler(object sender, EventArgs e)
        {
            window.Close();


            if (!(e is KeyPressedEventArgs args) || args.PressedKey == null)
                return;

            JanetBlock sampleBlock = BlockManager.GetAllBlocks().FirstOrDefault(x => x.Hotkey.Equals(args.PressedKey));
            try
            {
                if (sampleBlock != null && sampleBlock.Hotkey == args.PressedKey)
                {
                    Dispatcher.CurrentDispatcher.Invoke(() =>
                    {
                        QuietRoslynScriptHandler handler = new QuietRoslynScriptHandler(_commandData.Application);
                        handler.RunCode(sampleBlock.Code);
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void SetParameterValue(FamilyInstance instance, string parameterName, string parameterValue)
        {
            using (Transaction tr = new Transaction(instance.Document, "Change parameter value"))
            {
                tr.Start();
                Parameter param = instance.LookupParameter(parameterName);
                param.Set(parameterValue);
                tr.Commit();
            }
        }

        private void CreateDataParameter(FamilyInstance instance, string parameterName)
        {
            Document doc = instance.Document;
            Family ownerFamily = instance.Symbol.Family;
            Document familyDoc = doc.EditFamily(ownerFamily);

            using (Transaction tr = new Transaction(familyDoc, "Add new parameter and reload family"))
            {
                tr.Start();
                familyDoc.FamilyManager.AddParameter(parameterName, BuiltInParameterGroup.PG_DATA, ParameterType.Text,
                    true);
                familyDoc.LoadFamily(doc, new OverwriteFamilyOptions());
                tr.Commit();
            }

            familyDoc.Close(false);
        }
        
        public static string GetPath()
        {
            return typeof(CatchKeyboardShortcutCommand).Namespace + "." + nameof(CatchKeyboardShortcutCommand);
        }

        public static string GetAssemblyLocation() =>
            Assembly.GetExecutingAssembly().Location;
    }
}