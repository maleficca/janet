using JanetRevit.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace JanetRevit.Core.Helpers
{
    public static class BlockManager
    {
        public static List<JanetBlock> GetAllBlocks()
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Janet Blocks/";
            List<string> files = Directory.EnumerateFiles(basePath, "*.cs")
                .ToList();
            List<JanetBlock> blocks = new List<JanetBlock>();

            Regex codeRegex = new Regex(@"(?<=\/\/KeyCode:).*?(?=\n)");
            Regex nameRegex = new Regex(@"(?<=\/\/Name:).*?(?=\n)");

            foreach (string file in files)
            {
                string fileContents = File.ReadAllText(file);
                Match name = nameRegex.Match(fileContents);

                JanetBlock newBlock = new JanetBlock()
                {
                    Name = nameRegex.Match(fileContents).Groups[0].Value,
                    Hotkey = codeRegex.Match(fileContents).Groups[0].Value,
                    Code = fileContents,
                    FilePath = file
                };

                if (newBlock.Name.EndsWith("\r"))
                {
                    newBlock.Name = newBlock.Name.Substring(0, newBlock.Name.Length - 1);
                }

                if (newBlock.Hotkey.EndsWith("\r"))
                {
                    newBlock.Hotkey = newBlock.Hotkey.Substring(0, newBlock.Hotkey.Length - 1);
                }

                blocks.Add(newBlock);
            }

            return blocks;
        }

    }
}
