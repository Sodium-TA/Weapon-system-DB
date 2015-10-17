namespace WeaponSystem.Models
{
    using System.ComponentModel.DataAnnotations;

   public class Manufacturer
    {
       public Manufacturer()
       {
           
       }

       [Key]
       public int Id { get; set; }

       [MaxLength(40)]
       [Required]
       public string Name { get; set; }
    }
}
