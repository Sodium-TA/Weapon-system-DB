using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Ionic.Zlib;

namespace Readers
{
    class Program
    {
        static void Main()
        {
            FileUnziper.UnzipFile("Debug.zip", "UnZippedFiles");

            var megaCollection = new List<List<List<string>>>(); 

            foreach (var file in FileUnziper.Files)
            {
                var excelContent = ExcelReader.ReadExcelFile(file);
                // Still in test mode.. all this will be extracted as method
                string xmlAsString = excelContent.GetXml();

               megaCollection.Add(XmlReader.ParseXml(xmlAsString));
            }

            Console.WriteLine(megaCollection.Count);
        }
    }
}
