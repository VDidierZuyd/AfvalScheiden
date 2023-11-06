using System.ComponentModel.DataAnnotations;

namespace AfvalScheiden.Models
{
    public class Government
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string PostalCode { get; set; }
    }
}
