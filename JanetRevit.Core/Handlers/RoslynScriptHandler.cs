using Autodesk.Revit.UI;
using JanetRevit.Core.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace JanetRevit.Core.Handlers
{
    public class ScriptGlobals
    {
        public UIApplication _uiapp;
    }

    public class QuietRoslynScriptHandler
    {
        private readonly UIApplication _uiapp;

        public QuietRoslynScriptHandler()
        {
        }

        public QuietRoslynScriptHandler(UIApplication uiapp)
        {
            _uiapp = uiapp;
        }

        public void RunCode(string payload)
        {
            var assembliesToRef = new List<Assembly>
            {
                typeof(object).Assembly, //mscorlib
                typeof(Autodesk.Revit.UI.UIApplication).Assembly, // Microsoft.CodeAnalysis.Workspaces
                typeof(Autodesk.Revit.DB.Document).Assembly,
                typeof(JObject).GetTypeInfo().Assembly,
                typeof(OverwriteFamilyOptions).Assembly,
                typeof(IJanetBlock).Assembly,
            };

            var namespaces = new List<string>
            {
                "Autodesk.Revit.UI",
                "Autodesk.Revit.DB",
                "Autodesk.Revit.DB.Structure",
                "Autodesk.Revit.DB.ExtensibleStorage",
                "System",
                "System.Collections.Generic",
                "System.IO",
                "System.Linq",
                "Newtonsoft.Json",
                "Newtonsoft.Json.Linq",
                "JanetRevit.Core.Interfaces",
                "JanetRevit.Core.Handlers"
            };

            var options = ScriptOptions.Default.AddReferences(assembliesToRef).WithImports(namespaces);
            ScriptGlobals globals = new ScriptGlobals() { _uiapp = this._uiapp };

            try
            {
                var result = (Type)CSharpScript.RunAsync(payload, options, globals).Result.ReturnValue;
                var runnable = (IJanetBlock)Activator.CreateInstance(result);
                runnable.Execute(_uiapp);
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.Message + ex.InnerException?.Message);
            }
        }
    }

    public interface IRunnable
    {
        void Run();
    }
}