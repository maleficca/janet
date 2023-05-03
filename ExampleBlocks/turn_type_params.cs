//KeyCode:KEY_P
//Name:Turn instance to type parameters
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
        ChangeParametersToTypeParams(parameterSet, doc);
        int maxPasses = 5;
        int i = 0;
        while (parameterSet.Where(x => x.IsInstance == true).Any())
        {
            ChangeParametersToTypeParams(parameterSet, doc);

            i++;
            if(i == maxPasses) { break; }
        }
    }

    private void ChangeParametersToTypeParams(List<FamilyParameter> parameterSet, Document doc)
    {
        using (Transaction tr = new Transaction(doc, "Change parameters type"))
        {
            tr.Start();
            foreach (FamilyParameter parameter in parameterSet)
            {
                try
                {
                    doc.FamilyManager.MakeType(parameter);

                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            tr.Commit();
        }

    }
}

return typeof(ParamsMacro);
