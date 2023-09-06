//KeyCode:KEY_C
//Name:views to NWC
public class ViewsToNWC : IJanetBlock
{
    public void Execute(UIApplication uiapp)
    {
		//This is the path where the Navisworks files will be exported
		var exportPath = @"C:\Users\karol.jedruszek\Documents\Janet Blocks";
		
		//This is the path to the txt file which contains names of the 3D views which have to be exported
		//Every view name should be its own line in the file
		var viewsTextFilePath = @"C:\Users\karol.jedruszek\Documents\Janet Blocks\Views_to_export.txt";
		
        if (OptionalFunctionalityUtils.IsNavisworksExporterAvailable() == false)
        {
            TaskDialog.Show("Info", "The Naviswork export plugin is not installed.\n\nPlease download it from the Autodesk website and install it before executing this command.");
            return;
        }

        if (viewsTextFilePath == null) return;

        string viewsFileContent = File.ReadAllText(viewsTextFilePath);
        if (string.IsNullOrWhiteSpace(viewsFileContent)) return;

        Document doc = uiapp.ActiveUIDocument.Document;
        FilteredElementCollector viewsCollector = new FilteredElementCollector(doc);
        var views = viewsCollector.OfClass(typeof(View)).ToElements();
        if (views.Count == 0) return;

        if (string.IsNullOrWhiteSpace(exportPath)) return;
        //var exportPath = Directory.GetParent(filePath).FullName;

        foreach (var view in views)
        {
            if (viewsFileContent.Contains(view.Name) == false) continue;
            var exportOption = new NavisworksExportOptions() { ExportScope = NavisworksExportScope.View, ViewId = view.Id };
            doc.Export(exportPath, view.Name, exportOption);
        }
    }
}

return typeof(ViewsToNWC);