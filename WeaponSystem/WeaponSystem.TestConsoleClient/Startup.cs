using WeaponSystem.Models;

namespace WeaponSystem.TestConsoleClient
{
    using System.Data.Entity;
    using System.Linq;
    using MsSql.Data;
    
    using MsSql.Data.Migrations;
    using Readers;

    public class Startup
    {
        public static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WeaponSystemContext, Configuration>());

            var db = new WeaponSystemContext();
           db.Weapons.ToList();
            db.SaveChanges();

           var mongoHendler = new MondoDbHandler();
           mongoHendler.GetMongoData();


            var i = 0;
            var megaCollection = ExcelReader.GetExcelFilesAsCollection();
            var weaponsCat = db.WeaponCategoies.Find(1);
            
            foreach (var collection in megaCollection)
            {
                foreach (var weapon in collection)
                {
                    var weapons = new Weapon();

                    weapons.Name = weapon[0];
                    weapons.Id = i;
                    weapons.Manufacturer = null;
                    weapons.Description = null;
                    weapons.RelaseYear = 0;
                    weapons.WeaponCategory = weaponsCat;
                    weapons.ManufacturerId = null;
                    weapons.WeaponType = WeaponType.CloseRange;
                    weapons.Targets = null;
                    db.Weapons.Add(weapons);
                }
                i++;
            }

            db.SaveChanges();

        }
    }
}
