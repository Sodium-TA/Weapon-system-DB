namespace WeaponSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

   public class Manufacturer
    {
       private ICollection<Weapon> weapons;

       public Manufacturer()
       {
           this.weapons = new HashSet<Weapon>();
       }

       [Key]
       public int Id { get; set; }

       [MaxLength(40)]
       [Required]
       public string Name { get; set; }

       public int CountryId { get; set; }

       public virtual Country Country { get; set; }

       public virtual ICollection<Weapon> Weapons
       {
           get { return this.weapons; }
           set { this.weapons = value; }
       } 
    }
}
