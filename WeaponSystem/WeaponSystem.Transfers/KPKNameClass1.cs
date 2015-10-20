﻿namespace WeaponSystem.Transfers
{
    using System.Linq;
    using Readers;
    using Models;
    using MsSql.Data;
    using Parsers;

   public static class KPKNameClass1
    {
       public static void AddWeapons()
       {
           var db = new WeaponSystemContext();

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

                   if (!db.Weapons.Any(w => w.Name == weapon.Name))
                   {
                       db.Weapons.Add(weapon);
                   }
               }
           }

           db.SaveChanges();
       }

       public static void AddCalibers()
       {
           var db = new WeaponSystemContext();

           var caliberCollection = XmlReader.ReadXmlCollectionFromFile("../../../../Weapons Source Data/calibers.xml");

           foreach (var caliberItem in caliberCollection)
           {
               var caliber = new Caliber();
               caliber.Name = caliberItem[0];

               if (!db.Calibers.Any(x => x.Name == caliber.Name))
               {
                   db.Calibers.Add(caliber);
               }
           }

           db.SaveChanges();
       }
    }
}