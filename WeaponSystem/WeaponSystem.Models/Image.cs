namespace WeaponSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string OriginalName { get; set; }

        [Required]
        [MaxLength(10)]
        public string OriginalExtension { get; set; }

        [Required]
        public byte[] BinaryDataBytes { get; set; }
    }
}
