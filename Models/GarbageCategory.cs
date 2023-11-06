using System.ComponentModel.DataAnnotations;

namespace AfvalScheiden.Models
{
    public class GarbageCategory
    {
        public int Id {  get; set; }
        [Required]
        public string Name { get; set; }
    }
}
