﻿namespace Record_Store.Entity
{
    public class Order
    {
        public uint ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeliveryDate { get; set; } = DateTime.UtcNow.AddMonths(2);

        public bool IsActive { get; set; } = true;
    }
}
