---
sidebar_position: 5
---

# Model beams 

| Name                 | Place beams above doors and windows  |
|----------------------|---|
| Link                 |  [Github](https://github.com/vtdevelopment/janet-revit/blob/main/ExampleBlocks/model_lintels.cs) |
| Default hotkey       | `L`  |
| Description          | Gets all door and window instances in the project and places a void family and a beam family above it. |
| Configurable options | `beamHeight` (feet) - the height of the modelled beam. This determines the height of the void family instance.<br/>`extensionLength` (feet) - determines how much the modelled beam should extend on both sides of the door/window.<br/>`placementOffset` (feet) - determines if there should be an offset between the door opening and the bottom of the beam.<br/>`voidFamilyName` - name of the void family.<br/>`voidTypeName` - name of the void family type.<br/>`lintelFamilyAndTypeName` - name of the family and type of the beam family.  |