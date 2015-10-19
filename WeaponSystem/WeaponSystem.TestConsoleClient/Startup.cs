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

            var caliberCollection = XmlReader.ReadXmlFile("../../../../Weapons Source Data/calibers.xml");
            var test2 = XmlReader.ReadXmlCollectionFromFile("../../../../Weapons Source Data/targets.xml");

            foreach (var caliberItem in caliberCollection)
            {
                var caliber = new Caliber();
                caliber.Name = caliberItem;
                db.Calibers.Add(caliber);
            }

            var megaCollection = ExcelReader.GetExcelFilesAsCollection("../../../../Weapons Source Data/w.zip");


            var weaponsCat = db.WeaponCategoies.ToList();
            var manufacturers = db.Manufacturers.ToList();
            var calibers = db.Calibers.ToList();


            foreach (var collection in megaCollection)
            {
                foreach (var weaponItem in collection)
                {
                    var weapon = new Weapon();

                    weapon.Name = weaponItem[1];
                    weapon.Manufacturer = GetManufacturer(manufacturers, weaponItem[2]);
                    weapon.Description = null;
                    weapon.RelaseYear = 0;
                    weapon.WeaponCategory = GetCategory(weaponsCat, weaponItem[0]);
                    weapon.ManufacturerId = null;
                    weapon.WeaponType = WeaponType.CloseRange;
                    weapon.ImageUrl = weaponItem[4];
                    weapon.Targets = null;
                    weapon.Caliber = GetCaliber(calibers, weaponItem[3]);
                    db.Weapons.Add(weapon);

                    Console.WriteLine(weapon.Name);
                }
            }

            db.SaveChanges();
        }

        private static Caliber GetCaliber(List<Caliber> calibersCollection, string caliberSize)
        {
            foreach (var caliber in calibersCollection)
            {
                if (caliber.Name == caliberSize)
                {
                    return caliber;
                }
            }

            return null;
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

        private static Manufacturer GetManufacturer(List<Manufacturer> manufacturerCollection, string manufacturerName)
        {
            foreach (var manufacturer in manufacturerCollection)
            {
                if (manufacturer.Name == manufacturerName)
                {
                    return manufacturer;
                }
            }

            return null;
        }

    }
}
