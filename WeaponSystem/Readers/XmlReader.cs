﻿namespace Readers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;

    public static class XmlReader
    {
        public static List<List<string>> ParseXml(string xmlAsString)
        {
            var weaponsCollection = new List<List<string>>();

            var xmlDoc = XDocument.Parse(xmlAsString);
            var element = xmlDoc.Descendants().Skip(1);


            foreach (var collection in element)
            {
                var currentWeapon = new List<string>();
                var i = 0;
                foreach (var subCollection in collection.Descendants())
                {
                    if (i == 0)
                    {
                        var cleanedName = XmlConvert.DecodeName(subCollection.Parent.Name.ToString());
                        var categoryName = cleanedName;

                        Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                        categoryName = rgx.Replace(cleanedName, "");

                        currentWeapon.Add(categoryName);
                    }

                    currentWeapon.Add(subCollection.Value);
                    i++;
                }

                if (i != 0)
                {
                    weaponsCollection.Add(currentWeapon);
                }
            }

            return weaponsCollection;
        }

        public static List<List<string>> ReadXmlCollectionFromFile(string filePath)
        {
            var targetCollection = new List<List<string>>();
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            var xmlString = doc.InnerXml;

            var document = XDocument.Parse(xmlString);
            var test = document.Descendants().Skip(1);

            foreach (var targetItem in test)
            {
                var i = 0;
                var currenTarget = new List<string>();
                foreach (var target in targetItem.Descendants())
                {
                    currenTarget.Add(target.Value);
                    i++;
                }

                if (i != 0)
                {
                    targetCollection.Add(currenTarget);
                }
            }

            return targetCollection;
        } 
    }
}

