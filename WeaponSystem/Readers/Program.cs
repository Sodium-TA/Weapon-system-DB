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
            foreach (var file in FileUnziper.Files)
            {

                var excelContent = ExcelReader.ReadExcelFile(file);
                // Still in test mode.. all this will be extracted as method
                string xmlAsString = excelContent.GetXml();

                var collection = new List<List<string>>();

                var xmlDoc = XDocument.Parse(xmlAsString);
                var element = xmlDoc.Descendants().Skip(1);


                foreach (var test in element)
                {

                    var currentWeapon = new List<string>();
                    var i = 0;
                    foreach (var sub in test.Descendants())
                    {
                        currentWeapon.Add(sub.Value);
                        i++;
                    }

                    if (i != 0)
                    {
                        collection.Add(currentWeapon);
                    }

                }
                Console.WriteLine(collection.Count);
            }
        }
    }
}
