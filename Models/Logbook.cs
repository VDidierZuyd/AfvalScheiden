namespace AfvalScheiden.Models
{
    public class Logbook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Garbage>? garbages { get; set; }
        public Person? Person { get; set; }
        public int? PersonId { get; set; }
    }
}


