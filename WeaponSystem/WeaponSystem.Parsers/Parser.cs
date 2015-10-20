namespace WeaponSystem.Parsers
{
    using System.Collections.Generic;
    using Models;

    public static class Parser
    {
        public static Caliber GetCaliber(List<Caliber> calibersCollection, string caliberSize)
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

        public static WeaponCategory GetCategory(List<WeaponCategory> categoryCollection, string weaponCategoryName)
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

        public static Manufacturer GetManufacturer(List<Manufacturer> manufacturerCollection, string manufacturerName)
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

        public static WeaponType GetWeaponType(string weaponCategory)
        {
            switch (weaponCategory)
            {
                case "Pistols": return WeaponType.CloseRange;
                case "Shotguns": return WeaponType.CloseRange;
                case "Submachene Guns": return WeaponType.CloseRange;
                case "Rifles": return WeaponType.MediumRange;
                case "Machine Guns": return WeaponType.MediumRange;
                case "Sniper Rifles": return WeaponType.LongRange;
                default: return WeaponType.CloseRange;
            }
        }
    }
}
