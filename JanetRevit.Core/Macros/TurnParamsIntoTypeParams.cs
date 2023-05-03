using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using JanetRevit.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

public class ParamsMacro : IJanetBlock
{
    public void Execute(UIApplication uiapp)
    {
        Document doc = uiapp.ActiveUIDocument.Document;
        if (!doc.IsFamilyDocument)
        {
            return;
        }

        List<FamilyParameter> parameterSet = doc.FamilyManager.GetParameters().ToList();
        using(Transaction tr = new Transaction(doc, "Change parameters type"))
        {
            tr.Start();
            foreach (FamilyParameter parameter in parameterSet)
            {
                doc.FamilyManager.MakeType(parameter);
            }
            tr.Commit();
        }

    }
}

//return typeof(ParamsMacro);