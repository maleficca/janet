using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExtensibleStorage;
using Autodesk.Revit.UI;
using JanetRevit.Core.Interfaces;
using System;

//KEY_CODE:KEY_S
//Name:Extensible storage test
public class SaveDataInExtensibleStorage : IJanetBlock
{
    public void Execute(UIApplication uiapp)
    {
        Document doc = uiapp.ActiveUIDocument.Document;

        using (Transaction tr = new Transaction(doc, "Save data in storage"))
        {
            tr.Start();

            SchemaBuilder schemaBuilder =
            new SchemaBuilder(new Guid("720080CB-DA99-40DC-9415-E53F280AA1F0"));
            schemaBuilder.SetReadAccessLevel(AccessLevel.Public); // allow anyone to read the object
            schemaBuilder.SetWriteAccessLevel(AccessLevel.Vendor); // restrict writing to this vendor only
            schemaBuilder.SetVendorId("Janet"); // required because of restricted write-access
            schemaBuilder.SetSchemaName("ImportElementData");
            FieldBuilder fieldBuilder =
                    schemaBuilder.AddSimpleField("ImportData", typeof(String));
            Schema schema = schemaBuilder.Finish(); // register the Schema object
            Entity entity = new Entity(schema); // create an entity (object) for this schema (class)
                                                // get the field from the schema
            Field fieldSpliceLocation = schema.GetField("ImportElementData");
            String dataToStore = "Test data";
            // set the value for this entity
            
            entity.Set(fieldSpliceLocation, dataToStore);

            doc.SiteLocation.SetEntity(entity); // store the entity in the element

            // get the data back from the wall
            //Entity retrievedEntity = wall.GetEntity(schema);
            //XYZ retrievedData =
            //        retrievedEntity.Get<XYZ>(schema.GetField("WireSpliceLocation"),
            //        DisplayUnitType.DUT_METERS);

            tr.Commit();
        }
    }
}

//return typeof(SaveDataInExtensibleStorage);