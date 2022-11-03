using Record_Store.Auth;
using System.ComponentModel.DataAnnotations;

namespace Record_Store.Entity
{
    public class Order : IUserOwnedResource
    {
        public uint ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeliveryDate { get; set; } = DateTime.UtcNow.AddMonths(2);
        public bool IsActive { get; set; } = true;
        [Required]
        public string UserId { get; set; }
        public StoreRestUser User { get; set; }
    }
}
