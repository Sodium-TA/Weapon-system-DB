namespace Weapons.MySqlDb.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class WeaponReports
    {

        public WeaponReports()
        {
        }

        public int ID { get; set; }

        [Required]
        public int WeaponID { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string WeaponName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Manufacturer { get; set; }
    }
}