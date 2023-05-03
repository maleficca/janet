//KeyCode:KEY_L
//Name:Model Lintels
public class ModelLintelsMacro : IJanetBlock
{
    public void Execute(UIApplication uiapp)
    {
        Document doc = uiapp.ActiveUIDocument.Document;
        double beamHeight = 0.635;
        double extensionLength = 1;
        double placementOffset = 1;
        string voidFamilyName = "Wall_Opening"; 
        string voidTypeName = "Opening"; 
        string lintelFamilyAndTypeName = "8x8 Bond Beam"; 
        
        List<FamilyInstance> doorsAndWindows = new List<FamilyInstance>(0); 
        doorsAndWindows.AddRange(GetAllFamilyInstancesOfCategory(doc, BuiltInCategory.OST_Windows)); 
        doorsAndWindows.AddRange(GetAllFamilyInstancesOfCategory(doc, BuiltInCategory.OST_Doors)); 
        using (TransactionGroup group = new TransactionGroup(doc, "Place lintels")) 
        { 
            group.Start();
            doorsAndWindows.ForEach(window => 
            { 
                try 
                { 
                    XYZ windowLocation = (window.Location as LocationPoint).Point;
                    double levelDifference = (doc.GetElement(window.LevelId) as Level).Elevation - (doc.GetElement(window.Host.LevelId) as Level).Elevation;
                    double windowTop = window.LookupParameter("Sill Height").AsDouble() + window.Symbol.LookupParameter("Height").AsDouble() + levelDifference;
                    double windowWidth = window.Symbol.LookupParameter("Width").AsDouble(); FamilySymbol symbol = GetFamilySymbolByName(doc, voidFamilyName, voidTypeName);
                    if (symbol == null)
                    { 
                        TaskDialog.Show("Error", $"Cannot find famil {voidFamilyName} with type {voidTypeName}");
                        return;
                    } 
                    
                    FamilyInstance voidInstasnce = PlaceFamilyInstance(doc, symbol, windowLocation, window.Host);
                    double elevation = windowTop + placementOffset - voidInstasnce.LookupParameter("Sill Height").AsDouble();
                    SetInstanceParameterNumericValue(doc, voidInstasnce, BuiltInParameter.INSTANCE_ELEVATION_PARAM, elevation);
                    SetInstanceParameterNumericValue(doc, voidInstasnce, "Height", beamHeight);
                    SetInstanceParameterNumericValue(doc, voidInstasnce, "Width", windowWidth + extensionLength * 2);
                    XYZ from = windowLocation - window.HandOrientation * (windowWidth / 2 + extensionLength);
                    XYZ to = windowLocation + window.HandOrientation * (windowWidth / 2 + extensionLength);
                    Line loc = Line.CreateBound(from, to);
                    FamilyInstance lintel = PlaceBeam(doc, GetFamilySymbolByName(doc, lintelFamilyAndTypeName, lintelFamilyAndTypeName), loc, doc.GetElement(window.LevelId) as Level);
                    double voidTop = windowTop + beamHeight + placementOffset; SetInstanceParameterNumericValue(doc, lintel, BuiltInParameter.STRUCTURAL_BEAM_END0_ELEVATION, voidTop);
                    SetInstanceParameterNumericValue(doc, lintel, BuiltInParameter.STRUCTURAL_BEAM_END1_ELEVATION, voidTop);
                } catch (Exception ex)
                { 
                    TaskDialog.Show("Error", ex.Message + ex.StackTrace); 
                } 
            }); 
            group.Assimilate();
        }
    }

    public FamilyInstance PlaceFamilyInstance(
        Document doc, FamilySymbol familySymbol, XYZ location, Element host = null,
        Level level = null, Autodesk.Revit.DB.Structure.StructuralType structralType = Autodesk.Revit.DB.Structure.StructuralType.NonStructural)
    {
        FamilyInstance fi = null;
        using (Transaction tr = new Transaction(doc, "Place family instance"))
        {
            tr.Start();
            familySymbol.Activate();
            if (host != null)
                fi = doc.Create.NewFamilyInstance(location, familySymbol, host, structralType);
            else if (level != null)
                fi = doc.Create.NewFamilyInstance(location, familySymbol, level, structralType);
            else fi = doc.Create.NewFamilyInstance(location, familySymbol, structralType);
            tr.Commit();
        }
        return fi;
    }

    public List<FamilyInstance> GetAllFamilyInstancesOfCategory(Document doc, BuiltInCategory category) =>
        new FilteredElementCollector(doc)
        .OfCategory(category)
        .OfClass(typeof(FamilyInstance))
        .Select(el => el as FamilyInstance).ToList();

    public FamilySymbol GetFamilySymbolByName(Document doc, string familyName, string familyType) =>
        new FilteredElementCollector(doc)
        .OfClass(typeof(FamilySymbol))
        .Where(x => (x as FamilySymbol).FamilyName == familyName)
        .Where(x => x.Name.Equals(familyType)).FirstOrDefault() as FamilySymbol;

    public void SetInstanceParameterNumericValue(
        Document doc, FamilyInstance familyInstance, string parameterName, double value)
    {
        using (Transaction t = new Transaction(doc, "Set Parameter Value"))
        {
            t.Start();
            familyInstance.LookupParameter(parameterName).Set(value);
            t.Commit();
        }
    }

    public void SetInstanceParameterNumericValue(
        Document doc, FamilyInstance familyInstance, BuiltInParameter parameter, double value)
    {
        using (Transaction t = new Transaction(doc, "Set Parameter Value"))
        {
            t.Start();
            familyInstance.get_Parameter(parameter).Set(value);
            t.Commit();
        }
    }

    public FamilyInstance PlaceBeam(Document doc, FamilySymbol familySymbol, Line location, Level level,
        Autodesk.Revit.DB.Structure.StructuralType structralType = Autodesk.Revit.DB.Structure.StructuralType.Beam)
    {
        FamilyInstance fi = null;
        using (Transaction tr = new Transaction(doc, "Place family instance"))
        {
            tr.Start();
            familySymbol.Activate();
            fi = doc.Create.NewFamilyInstance(location, familySymbol, level, structralType);
            tr.Commit();
        }
        return fi;
    }
}
return typeof(ModelLintelsMacro);