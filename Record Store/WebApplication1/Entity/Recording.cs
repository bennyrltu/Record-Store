using Record_Store.Auth;
using System.ComponentModel.DataAnnotations;

namespace Record_Store.Entity
{
    public class Recording
    {
        public uint ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdated { get; set; }
        public bool IsActive { get; set; }
        public uint OrderId { get; set; }
        public Order Order { get; set; }
    }
}
