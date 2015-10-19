using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
       
            db.SaveChanges();
            //var test4 = ExcelReader.GetExcelFilesAsCollection("../../w.zip");
       
            var i = 0;
            var megaCollection = ExcelReader.GetExcelFilesAsCollection("../../w.zip");
            var weaponsCat = db.WeaponCategoies.ToList();

            foreach (var collection in megaCollection)
            {
                foreach (var weaponItem in collection)
                {
                    var weapon = new Weapon();

                    weapon.Name = weaponItem[1];
                    weapon.Manufacturer = null;
                    weapon.Description = null;
                    weapon.RelaseYear = 0;
                    weapon.WeaponCategory = GetCategory(weaponsCat, weaponItem[0]);
                    weapon.ManufacturerId = null;
                    weapon.WeaponType = WeaponType.CloseRange;
                    weapon.Targets = null;
                    db.Weapons.Add(weapon);

                    Console.WriteLine(weapon.Name);
                }

                i++;
            }

            db.SaveChanges();
        }

        private static WeaponCategory GetCategory(List<WeaponCategory> categoryCollection, string weaponCategoryName)
        {
            foreach (var category in categoryCollection)
            {
                if (category.Name == weaponCategoryName)
                {
                    return category;
                }
            }

            return null;
        }

    }
}
