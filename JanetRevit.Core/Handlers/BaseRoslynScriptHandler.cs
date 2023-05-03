using JanetRevit.Core.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JanetRevit.Core.Handlers
{
    public class BaseRoslynScriptHandler
    {
        public BaseRoslynScriptHandler() { }
        public void RunBlockCode(string payload)
        {
            var assembliesToRef = new List<Assembly>
            {
                typeof(object).Assembly, //mscorlib
                typeof(JObject).GetTypeInfo().Assembly,
                typeof(IJanetBlock).Assembly,
                typeof(MessageBox).Assembly
            };

            var namespaces = new List<string>
            {
                "System",
                "System.Collections.Generic",
                "System.IO",
                "System.Linq",
                "Newtonsoft.Json",
                "Newtonsoft.Json.Linq",
                "JanetRevit.Core.Interfaces",
                "System.Windows"
            };

            var options = ScriptOptions.Default.AddReferences(assembliesToRef).WithImports(namespaces);
            ScriptGlobals globals = new ScriptGlobals();

            try
            {
                var result = (Type)CSharpScript.RunAsync(payload, options, globals).Result.ReturnValue;
                var runnable = (IJanetBlockEmpty)Activator.CreateInstance(result);
                runnable.Execute();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException?.Message, "Error");
            }

        }
    }
}
