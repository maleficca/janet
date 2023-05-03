using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using JanetRevit.Core.Filters;

namespace JanetRevit.Core.RvtTasks
{
    public static class RvtUtil
    {
        public static Family GetFamily(Document doc, string familyName)
        {
            FilteredElementCollector a = new FilteredElementCollector(doc).OfClass(typeof(Family));

            Family family = a.FirstOrDefault<Element>(e => e.Name.Equals(familyName)) as Family;

            return family;
        }

        internal static List<Element> SelectWallsAndGrids(UIDocument uidoc)
        {
            return uidoc.Selection.PickObjects(ObjectType.Element, new WallAndGridSelectionFilter(), "Please select building walls and column grids")
                .Select(x => uidoc.Document.GetElement(x.ElementId))
                .Cast<Element>()
                .ToList();
        }

        public static List<FamilySymbol> GetFamilySymbols(Family fam)
        {
            List<FamilySymbol> FamSymbols = new List<FamilySymbol>();

            ISet<ElementId> familySymbolId = fam.GetFamilySymbolIds();
            foreach (ElementId id in familySymbolId)
            {
                if (fam.Document.GetElement(id) is FamilySymbol symbol)
                    FamSymbols.Add(symbol);
            }
            return FamSymbols;
        }

        public static FamilySymbol GetFamilySymbol(Family fam, string symbolName)
        {
            return GetFamilySymbols(fam).Where(fs => fs.Name == symbolName).FirstOrDefault();
        }

        internal static GroupType GetGroupType(Document doc, string stairsGroupName) =>
            new FilteredElementCollector(doc)
            .OfClass(typeof(GroupType))
            .ToElements()
            .Cast<GroupType>()
            .Where(x => x.Name.Equals(stairsGroupName))
            .FirstOrDefault();

        internal static Dictionary<string, object> GetFloorViewPlans(Document doc)
        {
            List<ViewPlan> viewPlans = GetViewPlans(doc);
            viewPlans = viewPlans.Where(x => x.ViewType == ViewType.FloorPlan).ToList();

            Dictionary<string, object> viewPlansDict = new Dictionary<string, object>();

            foreach (ViewPlan viewPlan in viewPlans)
                viewPlansDict.Add(viewPlan.Name, viewPlan.Id);

            return viewPlansDict;
        }

        public static void LoadFamilyToFile(Document doc, string Path)
        {
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Load Family");
                doc.LoadFamily(Path, out Family family);
                tx.Commit();
            }
        }

        public static void UnloadFamilyToFile(Document doc, string Name)
        {
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Unload Family");
                Family family = RvtUtil.GetFamily(doc, Name);
                doc.Delete(family.Id);
                tx.Commit();
            }
        }

        public static XYZ GetBoundingBoxMiddlePoint(BoundingBoxXYZ bbox) =>
            new XYZ((bbox.Max.X + bbox.Min.X) / 2, (bbox.Max.Y + bbox.Min.Y) / 2, (bbox.Max.Z + bbox.Min.Z) / 2);

        internal static List<Family> GetFamilies(Document loadedDocument) =>
            new FilteredElementCollector(loadedDocument)
                .OfClass(typeof(Family))
                .ToElements()
                .Cast<Family>()
                .ToList();

        internal static void DrawModelLine(Document doc, Curve curve)
        {
            using (Transaction tr = new Transaction(doc, "Draw model line"))
            {
                tr.Start();
                doc.Create.NewModelCurve(curve, doc.ActiveView.SketchPlane);
                tr.Commit();
            }
        }

        public static bool FamilyIsLoaded(Document doc, string Name)
        {
            Family family = RvtUtil.GetFamily(doc, Name);

            if (family == null)
            {
                //         MessageBox.Show("not loaded");

                return false;
            }
            if (family != null)
            {
                //        MessageBox.Show("loaded");

                return true;
            }
            return false;
        }

        public static string GetFamiliesNames(FilteredElementCollector elementCollector)
        {
            string temp = string.Empty;

            foreach (Element element in elementCollector)
            {
                if (!(element.Name.Contains("Standart") ||
                      element.Name.Contains("Mullion") ||
                      element.Name.Contains("Tag")))
                {
                    Family family = element as Family;
                    temp += element.Name;

                    ISet<ElementId> familySymbolId = family.GetFamilySymbolIds();
                    foreach (ElementId id in familySymbolId)
                    {
                        if (family.Document.GetElement(id) is FamilySymbol symbol)
                            temp += "#" + symbol.Name;
                    }
                    temp += "\n";
                }
            }
            return temp;
        }

        public static FamilyInstance PlaceFamilyInstance(Autodesk.Revit.DB.Document document, Family Family)
        {
            XYZ location = new XYZ(0, 0, 0);
            return PlaceFamilyInstance(document, Family, location);
        }

        public static FamilyInstance PlaceFamilyInstance(Autodesk.Revit.DB.Document document, Family Family, XYZ Location)
        {
            FamilySymbol symbol = RvtUtil.GetFamilySymbols(Family).FirstOrDefault();
            FamilyInstance FamInst;
            using (Transaction tx = new Transaction(document))
            {
                tx.Start("Place Family");
                FamInst = document.Create.NewFamilyInstance(Location, symbol, RvtUtil.GetAllLevels(document).FirstOrDefault(), StructuralType.NonStructural);
                tx.Commit();
            }
            return FamInst;
        }

        internal static void CreateDimension(Document doc, Line line, Reference reference1, Reference reference2, bool isLocked)
        {
            ReferenceArray refArray = new ReferenceArray();
            refArray.Append(reference1);
            refArray.Append(reference2);
            Dimension dim1 = doc.Create.NewDimension(doc.ActiveView, line, refArray);
            dim1.IsLocked = isLocked;
        }

        public static List<FamilyInstance> PlaceFamilyInstances(Autodesk.Revit.DB.Document document, Family Family, List<XYZ> Locations)
        {
            List<FamilyInstance> FamInsts = new List<FamilyInstance>();

            foreach (var loc in Locations)
            {
                FamInsts.Add(RvtUtil.PlaceFamilyInstance(document, Family, loc));
            }
            return FamInsts;
        }

        public static List<FamilyInstance> GetFamilyInstaces(Autodesk.Revit.DB.Document document, List<ElementId> ElemIds)
        {
            List<FamilyInstance> FamInsts = new List<FamilyInstance>();

            foreach (var id in ElemIds)
            {
                try
                {
                    FamInsts.Add(document.GetElement(id) as FamilyInstance);
                }
                catch
                {
                }
            }
            return FamInsts;
        }

        public static List<ElementId> GetElementIds(List<FamilyInstance> fams)
        {
            List<ElementId> ids = new List<ElementId>();
            foreach (var f in fams)
            {
                ids.Add(f.Id);
            }
            return ids;
        }

        public static List<ElementId> GetNestedFamiliesIds(FamilyInstance famInst)
        {
            List<ElementId> nestFams = new List<ElementId>();
            if (famInst.SuperComponent == null)
            {
                // this is a family that is a root family
                // ie might have nested families
                // but is not a nested one
                var subElements = famInst.GetSubComponentIds();
                if (subElements.Count == 0)
                {
                    // no nested families
                    System.Diagnostics.Debug.WriteLine(famInst.Name + " has no nested families");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(famInst.Name + " has nested families " + nestFams.Count.ToString());

                    // has nested families
                    nestFams.AddRange(subElements);
                }
            }

            return nestFams;
        }

        public static void CreateAssembly(Document doc, List<ElementId> elemIds)
        {
            ElementId categoryId = null;
            ElementId keyElemId = null;

            if (elemIds.Count > 0)
            {
                foreach (ElementId elemId in elemIds)
                {
                    // Use category of one of the assembly elements
                    keyElemId = elemId;
                    categoryId = doc.GetElement(elemId).Category.Id;
                    break;
                }

                Transaction transaction = new Transaction(doc);

                // Check if the Category name is valid

                if (AssemblyInstance.IsValidNamingCategory(doc, categoryId, elemIds))
                {
                    transaction.Start("Create Assembly Instance");
                    AssemblyInstance assemblyInstance =
                      AssemblyInstance.Create(doc, elemIds, categoryId);

                    // Commit the transaction that creates the
                    // assembly instance before modifying the
                    // instance's name

                    transaction.Commit();

                    // Create another transaction for assigning name to the
                    // assembly instance

                    transaction.Start("Set Assembly Name");
                    assemblyInstance.AssemblyTypeName =
                      "Gondola Assembly" + keyElemId.ToString();
                    transaction.Commit();

                    // Create assembly views for this assembly instance

                    if (assemblyInstance.AllowsAssemblyViewCreation())
                    {
                        transaction.Start("View Creation");

                        // Create Orthographic view

                        AssemblyViewUtils.Create3DOrthographic(
                          doc, assemblyInstance.Id
                          );

                        // Create Part list
                        AssemblyViewUtils.CreatePartList(doc, assemblyInstance.Id);

                        transaction.Commit();
                    }
                }
            }
        }

        public static FilteredElementCollector WorksetElements(Document doc, Workset workset)
        {
            FilteredElementCollector elementCollector = new FilteredElementCollector(doc).OfClass(typeof(Family));
            ElementWorksetFilter elementWorksetFilter = new ElementWorksetFilter(workset.Id, false);
            return elementCollector.WherePasses(elementWorksetFilter);
        }

        public static Workset GetActiveWorkset(Document doc)
        {
            WorksetTable table = doc.GetWorksetTable();
            WorksetId activeId = table.GetActiveWorksetId();
            Workset workset = table.GetWorkset(activeId);
            return workset;
        }

        public static IList<Workset> GetAllWorksets(Document doc)
        {
            string message = string.Empty;
            FilteredWorksetCollector collector = new FilteredWorksetCollector(doc);
            collector.OfKind(WorksetKind.FamilyWorkset);
            IList<Workset> worksets = collector.ToWorksets();
            //if (worksets.Count == 0)
            //    TaskDialog.Show("Worksets", " No Worksets in project");
            foreach (Workset workset in worksets)
            {
                message += "Workset : " + workset.Name;
                message += "\nUnique Id : " + workset.UniqueId;
                message += "\nOwner : " + workset.Owner;
                message += "\nKind : " + workset.Kind;
                message += "\nIs default : " + workset.IsDefaultWorkset;
                message += "\nIs editable : " + workset.IsEditable;
                message += "\nIs open : " + workset.IsOpen;
                message += "\nIs visible by default : " + workset.IsVisibleByDefault + "\n";
                message += "\n\n";
                //TaskDialog.Show("GetWorksetsInfo", message);
            }
            return worksets;
        }

        public static List<Level> GetAllLevels(Document doc)
        {
            List<Level> levelCollection = new List<Level>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<Element> collection = collector.OfClass(typeof(Level)).ToElements();
            foreach (var e in collection)
            {
                if (e is Level level)
                {
                    // keep track of number of levels
                    levelCollection.Add(level);
                }
            }
            return levelCollection;
        }

        public static List<Reference> GetReferencesbyName(List<FamilyInstance> famInst, String Name)
        {
            List<Reference> Refs = new List<Reference>();

            foreach (var fi in famInst)
            {
                Refs.Add(fi.GetReferenceByName(Name));
            }
            return Refs;
        }

        internal static List<FloorType> GetFloorTypes(Document doc) =>
            new FilteredElementCollector(doc)
                .OfClass(typeof(FloorType))
                .ToElements()
                .Cast<FloorType>()
                .ToList();

        public static List<WallType> GetWallTypes(Document doc)
        {
            return new FilteredElementCollector(doc)
                .OfClass(typeof(WallType))
                .ToElements()
                .Cast<WallType>()
                .ToList();
        }

        public static List<RoofType> GetRoofTypes(Document doc)
        {
            return new FilteredElementCollector(doc)
                .OfClass(typeof(RoofType))
                .ToElements()
                .Cast<RoofType>()
                .ToList();
        }

        public static List<ViewPlan> GetViewPlans(Document loadedDocument) =>
            new FilteredElementCollector(loadedDocument)
                .OfClass(typeof(ViewPlan))
                .ToElements()
                .Cast<ViewPlan>()
                .ToList();

        public static List<Wall> SelectWalls(UIDocument uidoc)
        {
            return uidoc.Selection.PickObjects(ObjectType.Element, new WallSelectionFilter(), "Please select building walls")
                .Select(x => uidoc.Document.GetElement(x.ElementId))
                .Cast<Wall>()
                .ToList();
        }

        public static Wall PickWall(UIDocument uidoc)
        {
            return uidoc.Document.GetElement(
                uidoc.Selection.PickObject(ObjectType.Element, new WallSelectionFilter(), "Please select building wall").ElementId)
                as Wall;
        }

        public static List<Grid> SelectColumnGrids(UIDocument uidoc) =>
            uidoc.Selection.PickObjects(ObjectType.Element, new GridSelectionFilter(), "Please select reference grids")
            .Select(x => uidoc.Document.GetElement(x.ElementId))
            .Cast<Grid>()
            .ToList();

        public static List<Group> GetGroups(Document doc) =>
            new FilteredElementCollector(doc)
            .OfClass(typeof(Group))
            .ToElements()
            .Cast<Group>()
            .ToList();

        public static List<FamilyInstance> GetFamilyInstances(Document doc) =>
            new FilteredElementCollector(doc)
                .OfClass(typeof(FamilyInstance))
                .ToElements()
                .Cast<FamilyInstance>()
                .ToList();

        public static List<FamilyInstance> GetFamilyInstancesByCategory(Document doc, Category category) =>
            GetFamilyInstances(doc)
                .Where(x => x.Category == category)
                .ToList();

        public static List<FamilyInstance> GetFamilyInstanceBySymbolName(Document doc, string symbolName) =>
            GetFamilyInstances(doc)
                .Where(x => x.Symbol.Name.Equals(symbolName))
                .ToList();

        /// <summary>
        /// Sets Family Instance parameter. Requires active transaction to use.
        /// </summary>
        public static void SetFamilyInstanceParameter(FamilyInstance instance, string parameterName, string parameterValue) =>
            instance.LookupParameter(parameterName).SetValueString(parameterValue);

        /// <summary>
        /// Sets Family Instance parameter. Requires active transaction to use.
        /// </summary>
        public static void SetFamilyInstanceParameter(FamilyInstance instance, string parameterName, double parameterValue) =>
            instance.LookupParameter(parameterName).SetValueString(parameterValue.ToString());

        /// <summary>
        /// Gets all callout boxes placed on view.
        /// </summary>
        public static List<Element> GetCalloutBoxesFromView(View view) {
            Document doc = view.Document;
            List<Element> elements = new FilteredElementCollector(doc).WherePasses(new ElementCategoryFilter(BuiltInCategory.OST_Viewers)).ToList();
            List<Element> selectedElements = new List<Element>();
            foreach(Element viewElement in elements)
            {
                Parameter orientationParameter = viewElement.get_Parameter(BuiltInParameter.PLAN_VIEW_NORTH);
                BoundingBoxXYZ bbox = viewElement.get_BoundingBox(view);

                if (orientationParameter == null)
                    continue;
                else if(orientationParameter.AsInteger() == 2)
                {
                    selectedElements.Add(viewElement);
                }
            }

            return selectedElements;
        }

    }
}