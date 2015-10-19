namespace Weapons.MySqlDb.Actions
{
    using System;

    using Newtonsoft.Json.Linq;
    using Weapons.MySqlDb.Data;
    using Weapons.MySqlDb.Models;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Weapons.MySqlDb.Data.Migrations;

    public static class MySqlActions
    {
        public static void AddReport(JObject report, WeaponReportsDbContext db, bool isAddingMany = false)
        {
            var weaponReport = new WeaponReports();
            var weaponId = report.GetValue("weapon-id");
            var weaponName = report.GetValue("weapon-name");
            var manufacturer = report.GetValue("manufacturer");

            weaponReport.WeaponID = weaponId.Value<int>();
            weaponReport.WeaponName = weaponName.Value<string>();
            weaponReport.Manufacturer = manufacturer.Value<string>();

            db.WeaponReports.Add(weaponReport);
            if (!isAddingMany)
            {
                db.SaveChanges();
            }
        }

        public static void AddReports(IList<JObject> reports, WeaponReportsDbContext db)
        {
            for (int i = 0; i < reports.Count; i++)
            {
                if (i +1 == reports.Count)
                {
                    AddReport(reports[i], db);
                }
                else
                {
                    AddReport(reports[i], db, true);
                }
            }
        }
    }
}
