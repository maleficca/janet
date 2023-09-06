---
sidebar_position: 6
---

# Create families

| Name                 | Create families  |
|----------------------|---|
| Link                 |  [Github](https://github.com/vtdevelopment/janet-revit/blob/main/ExampleBlocks/family_create.cs) |
| Default hotkey       | `G`  |
| Description          | Creates empty Revit families based on `.csv` file and `.rft` template. |
| Configurable options | `outputPath` - the folder where the generated families will be saved.<br/>`csvFilePath` - path to the `.csv` file. The file should contain two columns named `Family Name` and `Family Category`. The first column determines the name of the generated family. The second one determines the family category. The separator for the `.csv` file should be a comma `,`.<br/>`familyTemplatePath` - the path to the `.rft` family template from which the families will be created. |