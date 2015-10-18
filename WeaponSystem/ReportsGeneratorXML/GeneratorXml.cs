namespace WeaponSystem.ReportsGeneratorXML
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using WeaponSystem.MsSql.Data;

    public static class GeneratorXml
    {
        private static void GenerateXmlReport()
        {
            var weapons = new WeaponSystemContext();

            var queryResult =
                from weapon in weapons.Weapons
                join manufacturer in weapons.Manufacturers
                on weapon.ManufacturerId equals manufacturer.Id
                select new
                {
                    WeaponName = weapon.Name,
                    WeaponManufacturer = manufacturer.Name
                };

            string reportLocation = "../../Report.xml";
            Encoding encoding = Encoding.GetEncoding("windows-1251");

            using (XmlTextWriter writer = new XmlTextWriter(reportLocation, encoding))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("weapons");

                foreach (var weapon in queryResult)
                {
                    writer.WriteStartElement("weapon");
                    writer.WriteAttributeString("name", weapon.WeaponName);
                    writer.WriteAttributeString("manufacturer", weapon.WeaponManufacturer);

                    writer.WriteEndElement();
                }

                writer.WriteEndDocument();
            }
        }

        public static void Main()
        {
            GenerateXmlReport();
            Console.WriteLine("XML Report successfully generated.");
        }
    }
}
