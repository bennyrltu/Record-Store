namespace Record_Store.Data.DTOS.Orders;

public record RecordDTO(uint ID, string Name, string Description, decimal Price, DateTime CreationDate);
public record CreateRecordDTO(string Name, string Description, decimal Price);
public record UpdateRecordDTO(string Name, string Description, decimal Price);