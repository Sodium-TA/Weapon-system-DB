namespace WeaponSystem.TestConsoleClient
{
    using System.Data.Entity;
    using System.Linq;
    using MsSql.Data;
    
    using MsSql.Data.Migrations;

    public class Startup
    {
        public static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WeaponSystemContext, Configuration>());

            //var db = new WeaponSystemContext();
            //db.Weapons.ToList();
            //db.SaveChanges();

          //  var mongoHendler = new MondoDbHandler();
           // mongoHendler.GetMongoData();

        }
    }
}
