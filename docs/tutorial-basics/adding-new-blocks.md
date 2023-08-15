---
sidebar_position: 4
---

# Adding new Blocks

New Blocks can be added by creating a new file in the `Janet Blocks` directory located in My Documents.
There are certain criteria a Block needs to keep in order to execute correctly in Janet.

- The class needs to implement `IJanetBlock` interface by having a public `Execute` method, which takes `UIApplication` object in the constructor.
- The `UIApplication` object is the base object for reaching into the Revit document and extracting/modifying data in it.
- The Block needs to return the class at the very end. This is because the Block compiles to the class implementing the `IJanetBlock` and then runs the `Execute` method. The Block needs to return the class object in order for the Roslyn library to compile it properly.
- Block needs to comments with metadata for name nad hotkey - `KeyCode` and `Name`.

Example Block, returning the title of the current Revit Document, could looks something like this:
```csharp
//KeyCode:KEY_A
//Name:Get document title
public class JanetTitleMacro: IJanetBlock
{
    public void Execute(UIApplication uiapp)
    {
        Document doc = uiapp.ActiveUIDocument.Document;
        TaskDialog.Show("Document title", doc.Title);
    }
}

return typeof(JanetTitleMacro);
```
