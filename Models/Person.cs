using System.ComponentModel.DataAnnotations;

namespace AfvalScheiden.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Government? Government { get; set; }
        public int GovernmentId { get; set; }
        public Logbook? Logbook { get; set; }
    }
}
