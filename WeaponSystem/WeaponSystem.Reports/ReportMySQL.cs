namespace WeaponSystem.Reports
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Newtonsoft.Json.Linq;
    using Weapons.MySqlDb.Actions;
    using Weapons.MySqlDb.Data;
    using WeaponSystem.MsSql.Data;

    public class ReportMySQL
    {
        private const string MySQLSuccessMessage = "MySQL Reports created successfully.";

        public async Task<string> GenerateMySQLReport()
        {
            using (WeaponReportsDbContext MySqlServerContext = new WeaponReportsDbContext())
            {
                var list = new List<JObject>();
                using (WeaponSystemContext msSqlServerContext = new WeaponSystemContext())
                {
                    await msSqlServerContext
                        .Weapons
                        .Select(w => new
                        {
                            Id = w.Id.ToString(),
                            Name = w.Name.ToString(),
                            Manufacturer = w.Manufacturer.Name.ToString()
                        })
                        .ForEachAsync(w =>
                            list.Add(CreateJObject(w.Id, w.Name, w.Manufacturer))
                        );
                }

                MySqlActions.AddReports(list, MySqlServerContext);
            }

            return MySQLSuccessMessage;
        }

        private JObject CreateJObject(string id, string name, string manufacturer)
        {
            var jsonObject = new JObject(
                 new JProperty("weapon-id", id),
                 new JProperty("weapon-name", name),
                 new JProperty("manufacturer", manufacturer));

            return jsonObject;
        }
    }
}
