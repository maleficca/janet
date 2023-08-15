//KeyCode:KEY_F
//Name:Extract FabricationData
public class FabricationDataMacro: IJanetBlock
{
    public void Execute(UIApplication uiapp)
    {
        var doc = uiapp.ActiveUIDocument.Document;
		var selectedElements = uiapp.ActiveUIDocument.Selection.GetElementIds().ToList();
		var firstElement = selectedElements.FirstOrDefault();
		
		if(firstElement == null){
			return;
		}
		
		var element = doc.GetElement(firstElement);
		
		if(element is FabricationPart){
			var part = element as FabricationPart;
			
            var dims = part.GetDimensions();

            var mappingDictionary = new Dictionary<string, string>(){
                {"C1", "VT_Ductwork End 1"},
                {"C2", "VT_Ductwork End 2"},
                {"Top Extension", "VT_Throat Length 1"},
				{"Bottom Extension", "VT_Throat Length 2"}
            };

            var paramValueDictionary = new Dictionary<string, string>();

            foreach (var dim in dims)
            {
                var value = part.GetDimensionValue(dim);
                string parameterName;

                if (mappingDictionary.TryGetValue(dim.Name, out parameterName))
                {
                    paramValueDictionary.Add(parameterName, value.ToString());
                }

            }

            var connectors = part.ConnectorManager.Connectors;
            foreach(var connector in connectors)
            {
                var id = ((Connector)connector).GetFabricationConnectorInfo().BodyConnectorId;
                var connectorId = ((Connector)connector).Id;
                var name = FabricationConfiguration.GetFabricationConfiguration(doc).GetFabricationConnectorName(id);
                var group = FabricationConfiguration.GetFabricationConfiguration(doc).GetFabricationConnectorGroup(id);
                string parameterName;

                if (mappingDictionary.TryGetValue($"C{connectorId + 1}", out parameterName))
                {
                    paramValueDictionary.Add(parameterName, $"{group}: {name}");
                }
                
            }

            using (Transaction tr = new Transaction(doc, "Modify Fabrication element parameters"))
            {
                tr.Start();

                foreach (var pair in paramValueDictionary)
                {
                    var param = part.LookupParameter(pair.Key);
					
                    if(param == null)
                    {
                        continue;
                    }
					
                    param.Set(pair.Value);
                }

                tr.Commit();
            }

		}
    }
	
}

return typeof(FabricationDataMacro);