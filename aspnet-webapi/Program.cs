using aspnet_webapi.Dtos;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

const string getEndpoint = "GetGame";

List<GameDTO> games = [
    new(
        1, 
        "Black Myth Wukong", 
        "RPG", 
        19.99M, 
        new DateOnly(2024, 8, 20)
    ),

    new(
        2, 
        "Final Fantasy XIV", 
        "RPG", 
        29.99M, 
        new DateOnly(2010, 9, 30)
    ),

    new(
        3, 
        "FIFA 23", 
        "Sports", 
        69.99M, 
        new DateOnly(2022, 9, 27)
    ),
];

// return list of games through GET  Request
// GET /games
app.MapGet("games", () => games);

// find games based on ID
// GET /games/{id}
app.MapGet("games/{id}", (int id) => {
    GameDTO? game = games.Find(game => game.Id == id);

    return game != null ? Results.Ok(game) : Results.NotFound();
}
).WithName(getEndpoint);

// POST /games
app.MapPost("games", (CreateGameDTO newGame) => {
    GameDTO game = new (
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    games.Add(game);

    return Results.CreatedAtRoute(getEndpoint, new{id = game.Id}, game);
});

// Update Games
// PUT /games/{id}
app.MapPut("games/{id}", (int id, UpdateGameDTO updatedGame) => {
    var index = games.FindIndex(game => game.Id == id);

    if(index == -1){
        return Results.NotFound();
    }

    games[index] = new GameDTO(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );

    return Results.NoContent();
});

// Delete a game from the list
// DELETE /games/{id}
app.MapDelete("games/{id}", (int id) => {
    games.RemoveAll(game => game.Id == id);

    return Results.Content("Successfully deleted");
});

//app.MapGet("/", () => "Hello World!");

app.Run();
