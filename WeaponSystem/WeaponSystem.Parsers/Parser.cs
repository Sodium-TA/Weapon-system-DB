using System.Linq;
using WeaponSystem.MsSql.Data;

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

        public static WeaponType GetWeaponType(List<WeaponType> weaponTypes, string weaponCategory)
        {
            var closeRangeType = weaponTypes.Find(x => x.Name == "Close Range");
            var mediumRangeType = weaponTypes.Find(x => x.Name == "Medium Range");
            var longRangeType = weaponTypes.Find(x => x.Name == "Long Range");

            switch (weaponCategory)
            {
                case "Pistols": return closeRangeType;
                case "Shotguns": return closeRangeType;
                case "Submachene Guns": return closeRangeType;
                case "Rifles": return mediumRangeType;
                case "Machine Guns": return mediumRangeType;
                case "Sniper Rifles": return longRangeType;
                default: return null;
            }
        }
    }
}
