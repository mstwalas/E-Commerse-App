using System.ComponentModel.DataAnnotations;

namespace E_Commerse_API.Models
{
    public class Catogry
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public bool Enable { get; set; }
        public bool Deleted { get; set; }
    }
}
