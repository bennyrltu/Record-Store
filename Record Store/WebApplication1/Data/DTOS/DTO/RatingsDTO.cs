namespace Record_Store.Data.DTOS.Orders;

public record RatingDTO(uint ID, string Name, int Rating, DateTime CreationDate);
public record CreateRatingDTO(string Name, int Rating);
public record UpdateRatingDTO(string Name, int Rating);