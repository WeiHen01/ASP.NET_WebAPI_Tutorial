namespace aspnet_webapi.Dtos;

public record class CreateGameDTO(
    int Id, 
    string Name, 
    string Genre, 
    decimal Price,
    DateOnly ReleaseDate
);

