using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanetRevit.Core.Interfaces
{
    public interface IJanetBlock
    {
        void Execute(UIApplication uiapp);
    }

    public interface IJanetBlockEmpty
    {
        void Execute();
    }
}
