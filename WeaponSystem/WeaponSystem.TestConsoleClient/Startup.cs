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
            db.Calibers.ToList();

            db.SaveChanges();

            var caliberCollection = XmlReader.ReadXmlFile("../../../../Weapons Source Data/calibers.xml");

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

            var test = db.Weapons.ToList();

            foreach (var collection in megaCollection)
            {
                foreach (var weaponItem in collection)
                {
                    var weapon = new Weapon();

                    var weaponName = weaponItem[1];
                    var weaponCategory = weaponItem[0];
                    var weaponManufacturer = weaponItem[2];
                    var weaponCaliber = weaponItem[3];
                    var weaponImage = weaponItem[4];

                    weapon.Name = weaponName;
                    weapon.Manufacturer = GetManufacturer(manufacturers, weaponManufacturer);
                    weapon.Description = null;
                    weapon.RelaseYear = 0;
                    weapon.WeaponCategory = GetCategory(weaponsCat, weaponCategory);
                    weapon.ManufacturerId = null;
                    weapon.WeaponType = GetWeaponType(weaponCategory);
                    weapon.ImageUrl = weaponImage;
                    weapon.Targets = null;
                    weapon.Caliber = GetCaliber(calibers, weaponCaliber);

                    

                    db.Weapons.Add(weapon);

                    Console.WriteLine(weapon.Name);
                }
            }

            db.SaveChanges();
        }

        private static WeaponType GetWeaponType(string weaponCategory)
        {
            switch (weaponCategory)
            {
                case "Pistols": return WeaponType.CloseRange;
                case "Shotguns": return WeaponType.CloseRange;
                case "Submachene Guns": return WeaponType.CloseRange;
                case "Rifles": return WeaponType.MediumRange;
                case"Machine Guns": return WeaponType.MediumRange;
                case"Sniper Rifles": return WeaponType.LongRange;
                default: return WeaponType.CloseRange;
            }

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
