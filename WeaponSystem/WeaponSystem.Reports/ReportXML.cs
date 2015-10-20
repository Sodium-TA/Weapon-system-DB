namespace WeaponSystem.Reports
{
    using System.Linq;
    using System.Text;
    using System.Xml;
    using WeaponSystem.MsSql.Data;

    public class ReportXml
    {
        private const string XmlSuccessMessage = "XML Report created successfully.";

        public string GenerateXmlReport()
        {
            var msSqlServerContext = new WeaponSystemContext();

            var queryResult = msSqlServerContext
                    .Weapons
                    .Select(w => new
                    {
                        Id = w.Id,
                        Name = w.Name,
                        Manufacturer = w.Manufacturer.Name,
                        Category = w.WeaponCategory.Name
                    });

            string reportLocation = "../../../../Generated Reports/XML/Report.xml";
            var encoding = Encoding.GetEncoding("windows-1251");

            using (var writer = new XmlTextWriter(reportLocation, encoding))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("weapons");

                foreach (var weapon in queryResult)
                {
                    writer.WriteStartElement("weapon");
                    writer.WriteAttributeString("id", weapon.Id.ToString());
                    writer.WriteAttributeString("name", weapon.Name);
                    writer.WriteAttributeString("manufacturer", weapon.Manufacturer);
                    writer.WriteAttributeString("category", weapon.Category);

                    writer.WriteEndElement();
                }

                writer.WriteEndDocument();
            }

            return XmlSuccessMessage;
        }
    }
}