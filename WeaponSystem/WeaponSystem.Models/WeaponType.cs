namespace WeaponSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class WeaponType
    {
        public WeaponType()
        {
            
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
    }
}
