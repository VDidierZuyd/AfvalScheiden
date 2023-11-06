using System.ComponentModel.DataAnnotations;

namespace AfvalScheiden.Models
{
    public class Garbage
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }   
        public string Description { get; set; }
        public int TimeNeeded { get; set; }
        public GarbageCategory? GarbageCategory { get; set; }
        public int GarbageCategoryId { get; set; }
        public Logbook? Logbook { get; set; }
        public int LogbookId { get; set; }
    }
}
