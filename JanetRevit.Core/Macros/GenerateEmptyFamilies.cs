using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using JanetRevit.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//KeyCode:KEY_G
//Name:Generate empty families
namespace JanetRevit.Core.Macros
{
    public class GenerateEmptyFamilies : IJanetBlock
    {
        string outputPath = @"D:\OutputFamilies\";
        string csvFilePath = @"D:\input.csv";
        string familyTemplatePath = @"D:\template.rft";

        public void Execute(UIApplication uiapp)
        {
            Document doc = uiapp.ActiveUIDocument.Document;

            if(!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            if(!File.Exists(csvFilePath) || !File.Exists(familyTemplatePath))
            {
                TaskDialog.Show("Error", "Cannot find CSV file or Family Template file! Please check the file paths in the Janet Block!");
            }

            List<List<string>> csvValues = ReadCsvFile(csvFilePath);
            using(TransactionGroup group = new TransactionGroup(doc, "Create new families"))
            {
                group.Start();
                for (int i = 1; i < csvValues[0].Count; i++)
                {
                    string familyName = csvValues[0][i].ToString();
                    if (String.IsNullOrEmpty(familyName))
                    {
                        continue;
                    }

                    Category cat = GetCategory(doc, csvValues[1][i]);

                    if (cat is null)
                    {
                        continue;
                    }

                    Document familyDoc = uiapp.Application.NewFamilyDocument(familyTemplatePath);
                    using (Transaction tr = new Transaction(familyDoc, "Change family category"))
                    {
                        tr.Start();
                        familyDoc.OwnerFamily.FamilyCategory = cat;
                        tr.Commit();
                    }
                    familyDoc.SaveAs($@"{outputPath}{familyName}.rfa");
                    familyDoc.Close(false);
                }
                group.Assimilate();
            }
        }

        private Category GetCategory(Document doc, string catName)
        {
            Category cat = null;
            foreach(Category category in doc.Settings.Categories)
            {
                if (category.Name.Equals(catName))
                {
                    cat = category;
                    break;
                }
            }

            return cat;
        }

        public List<List<string>> ReadCsvFile(string filePath)
        {
            List<string> listA = new List<string>();
            List<string> listB = new List<string>();

            using (var reader = new StreamReader(csvFilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    listA.Add(values[0]);
                    listB.Add(values[1]);
                }
            }

            return new List<List<string>>() { listA, listB };
        }
    }
}

//return typeof(GenerateEmptyFamilies);
