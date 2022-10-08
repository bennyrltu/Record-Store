namespace Record_Store.Entity
{
    public class Recording
    {
        public uint ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdated { get; set; }
        public bool IsActive { get; set; }
        public Order Order { get; set; }

    }
}
