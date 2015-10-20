using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WeaponSystem.Models;
using WeaponSystem.Parsers;

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

            //var caliberCollection = XmlReader.ReadXmlCollectionFromFile("../../../../Weapons Source Data/calibers.xml");
            //var test2 = XmlReader.ReadXmlCollectionFromFile("../../../../Weapons Source Data/targets.xml");

            //foreach (var caliberItem in caliberCollection)
            //{
            //    var caliber = new Caliber();
            //    caliber.Name = caliberItem[0];
            //    db.Calibers.Add(caliber);
            //}

            var megaCollection = ExcelReader.GetExcelFilesAsCollection("../../../../Weapons Source Data/w.zip");


            var weaponsCat = db.WeaponCategoies.ToList();
            var manufacturers = db.Manufacturers.ToList();
            var calibers = db.Calibers.ToList();


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
                    weapon.Manufacturer = Parser.GetManufacturer(manufacturers, weaponManufacturer);
                    weapon.Description = null;
                    weapon.RelaseYear = 0;
                    weapon.WeaponCategory = Parser.GetCategory(weaponsCat, weaponCategory);
                    weapon.ManufacturerId = null;
                    weapon.WeaponType = Parser.GetWeaponType(weaponCategory);
                    weapon.ImageUrl = weaponImage;
                    weapon.Targets = null;
                    weapon.Caliber = Parser.GetCaliber(calibers, weaponCaliber);

                    db.Weapons.Add(weapon);

                    Console.WriteLine(weapon.Name);
                }
            }

            db.SaveChanges();
        }
    }
}
