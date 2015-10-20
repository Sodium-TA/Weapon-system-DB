using WeaponSystem.Transfers;

namespace WeaponSystem.TestConsoleClient
{
    using System;
    using Models;
    using Parsers;
    using System.Data.Entity;
    using System.Linq;
    using MsSql.Data;

    using MsSql.Data.Migrations;
    using Readers;

    public class Startup
    {
        public static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WeaponSystemContext, Configuration>());

            var db = new WeaponSystemContext();

            db.SaveChanges();

            KPKNameClass1.AddCalibers();

            KPKNameClass1.AddWeapons();

            Console.ReadKey();
        }
    }
}
