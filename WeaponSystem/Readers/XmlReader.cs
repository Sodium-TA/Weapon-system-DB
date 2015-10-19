namespace Readers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public static class XmlReader
    {
        public static List<List<string>> ParseXml(string xmlAsString)
        {
            var collection = new List<List<string>>();

            var xmlDoc = XDocument.Parse(xmlAsString);
            var element = xmlDoc.Descendants().Skip(1);


            foreach (var test in element)
            {
                var currentWeapon = new List<string>();
                var i = 0;
                foreach (var sub in test.Descendants())
                {
                    i++;
                    currentWeapon.Add(sub.Value);
                }

                if (i != 0)
                {
                    collection.Add(currentWeapon);
                }
            }

            return collection;
        }
    }
}

