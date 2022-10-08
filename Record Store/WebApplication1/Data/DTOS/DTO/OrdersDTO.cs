namespace Record_Store.Data.DTOS.Orders;

public record OrderDTO(uint ID, string Name, decimal Price, DateTime CreationDate);
public record CreateOrderDTO(string Name, decimal Price);
public record UpdateOrderDTO(string Name);