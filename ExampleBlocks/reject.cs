//KeyCode:KEY_N
//Name:Reject Macro
public class RejectMacro : IJanetBlock
{
    public void Execute(UIApplication uiapp)
    {
        Document doc = uiapp.ActiveUIDocument.Document;
        List<ElementId> selectedElements = uiapp.ActiveUIDocument.Selection.GetElementIds().ToList();
        string parameterName = "Marked Incorrect by";

        foreach (ElementId id in selectedElements)
        {
            FamilyInstance instance = doc.GetElement(id) as FamilyInstance;
            if (instance == null) continue; 
            Parameter confirmParameter = instance.LookupParameter(parameterName); 
            if (confirmParameter == null) 
            { 
                Family ownerFamily = instance.Symbol.Family;
                Document familyDoc = doc.EditFamily(ownerFamily);
                using (Transaction tr = new Transaction(familyDoc, "Add new parameter and reload family"))
                { 
                    tr.Start(); 
                    familyDoc.FamilyManager.AddParameter(parameterName, BuiltInParameterGroup.PG_DATA, ParameterType.Text, true); 
                    familyDoc.LoadFamily(doc, new OverwriteFamilyOptions()); 
                    tr.Commit(); 
                } 
                familyDoc.Close(false); 
            }

            using (Transaction tr = new Transaction(instance.Document, "Change parameter value")) 
            { 
                tr.Start(); 
                Parameter param = instance.LookupParameter(parameterName); 
                param.Set($"{uiapp.Application.Username} {DateTime.Now.ToShortDateString()}"); 
                tr.Commit(); 
            }
        }
    }
}

return typeof(RejectMacro);