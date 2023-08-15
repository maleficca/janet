---
sidebar_position: 3
---

# Working with Blocks

Scripts and macros used by Janet will be always referred to as ***Blocks***.
Blocks can be found in My Documents folder, inside ***Janet Blocks*** folder.

Blocks are essentialy C# files. Here's an example Block:
```csharp
//KeyCode:KEY_T
//Name:Test
public class TestMacro: IJanetBlock
{
    public void Execute(UIApplication uiapp)
    {
        TaskDialog.Show("Welcome message", "A simple test!");
    }
}

return typeof(TestMacro);
```
If you're just using the Block and want to simply change its name or hotkey, what interests you is the metadata contained in the comments on the top of the file:
```csharp
//KeyCode:KEY_T
//Name:Test
```

- `KeyCode` is the hotkey to which the macro is mapped to. Any letter on the keyboard can be used, and needs to be specified using this syntax: `KEY_<INSERT_LETTER>`, e.g. `KEY_A`, `KEY_B`.
- `Name` is a human-readable name for the Block. It shows up in the Block Editor and allows you to locate the Block from the Editor.