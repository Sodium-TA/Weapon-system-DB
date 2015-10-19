namespace WeaponSystem.Models
{
    using System.ComponentModel.DataAnnotations;

   public class Caliber
    {
        public Caliber()
        {

        }

        [Key]
        public int Id { get; set; }

        [MaxLength(6)]
        [Required]
        public string Name { get; set; }
  
    }
}
