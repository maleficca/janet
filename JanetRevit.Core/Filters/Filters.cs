using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace JanetRevit.Core.Filters
{
    public class LineSelectionFilter : ISelectionFilter
    {
        public bool AllowReference(Reference reference, XYZ point) => false;
        public bool AllowElement(Element e) => 
            e.Category != null && 
            e.Category.Id == Category.GetCategory(e.Document, BuiltInCategory.OST_Lines).Id;
    }

    public class WallSelectionFilter : ISelectionFilter
    {
        public bool AllowReference(Reference reference, XYZ point) => false;
        public bool AllowElement(Element e) =>
            e.Category != null &&
            e.Category.Id == Category.GetCategory(e.Document, BuiltInCategory.OST_Walls).Id;
    }

    public class GridSelectionFilter : ISelectionFilter
    {
        public bool AllowReference(Reference reference, XYZ point) => false;
        public bool AllowElement(Element e) =>
            e.Category != null &&
            e.Category.Id == Category.GetCategory(e.Document, BuiltInCategory.OST_Grids).Id;
    }

    public class WallAndGridSelectionFilter : ISelectionFilter
    {
        public bool AllowReference(Reference reference, XYZ point) => false;
        public bool AllowElement(Element e) =>
            e.Category != null &&
            (e.Category.Id == Category.GetCategory(e.Document, BuiltInCategory.OST_Walls).Id ||
            (e.Category.Id == Category.GetCategory(e.Document, BuiltInCategory.OST_Grids).Id));
    }
}
