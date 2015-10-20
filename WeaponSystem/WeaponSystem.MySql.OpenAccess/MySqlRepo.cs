using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telerik.OpenAccess;

namespace WeaponSystem.MySql.OpenAccess
{
    public class MySqlRepo
    {
        public void ImportDbDataFromJson(string path)
        {
            var ctx = new FluentModel();

            using (ctx)
            {
                var files = Directory.GetFiles(path);

                var teams = ctx.GetAll<WeaponReport>().ToList();

                foreach (var file in files)
                {
                    var fileText = File.ReadAllText(file);
                    var teamReport = JsonConvert.DeserializeObject<WeaponReport>(fileText);

                    var teamReportDb = ctx.WeaponReports.FirstOrDefault(t => t.weaponName == teamReport.weaponName);
                    if (teamReportDb != null)
                    {
                        teamReportDb.weaponId = teamReport.weaponId;
                        teamReportDb.weaponName = teamReport.weaponName;
                        teamReportDb.manufacturer = teamReport.manufacturer;
                    }
                    else
                    {
                        ctx.Add(teamReport);
                        ctx.SaveChanges();
                    }
                }
            }
        }

        public void UpdateDatabase()
        {
            using (var context = new FluentModel())
            {
                var schemaHandler = context.GetSchemaHandler();
                this.EnsureDB(schemaHandler);
            }
        }

        /// <summary>
        /// Ensures database exists and if not creates one
        /// </summary>
        /// <param name="schemaHandler">Gets schemaHandler of type ISchemaHandler</param>
        private void EnsureDB(ISchemaHandler schemaHandler)
        {
            string script = null;
            if (schemaHandler.DatabaseExists())
            {
                script = schemaHandler.CreateUpdateDDLScript(null);
            }
            else
            {
                schemaHandler.CreateDatabase();
                script = schemaHandler.CreateDDLScript();
            }

            if (!string.IsNullOrEmpty(script))
            {
                schemaHandler.ExecuteDDLScript(script);
            }
        }
    }
}
