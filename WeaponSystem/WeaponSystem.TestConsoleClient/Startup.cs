namespace WeaponSystem.TestConsoleClient
{
    using System.Linq;
    using MsSql.Data;
    using System.Data.Entity;
    using MsSql.Data.Migrations;

    class Startup
    {
        static void Main(string[] args)
        {

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WeaponSystemContext, Configuration>());

            var db = new WeaponSystemContext();
            db.Weapons.ToList();
            db.SaveChanges();
        }
    }
}
