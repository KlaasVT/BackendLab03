var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(configuration);
builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<IOccasionRepository, OccasionRepository>();
builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<ISneakerRepository, SneakerRepository>();
builder.Services.AddTransient<ISneakerService, SneakerService>();
builder.Services.AddValidatorsFromAssemblyContaining<SneakerValidator>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/setup", async (ISneakerService sneakerService) =>
{
    await sneakerService.SetupData();
    return "Setup complete";
});

app.MapGet("api/v1/sneakers", async (ISneakerService sneakerService) =>
{
    var sneakers = await sneakerService.GetAllSneakers();
    return Results.Ok(sneakers);
});

app.MapGet("api/v1/sneakers/{id}", async (string id, ISneakerService sneakerService) =>
{
    var sneaker = await sneakerService.GetSneakerById(id);
    return Results.Ok(sneaker);
});

app.MapPost("api/v1/sneakers", async (Sneaker sneaker, ISneakerService sneakerService) =>
{
    var result = await sneakerService.AddSneaker(sneaker);
    return Results.Ok(result);
});

app.MapGet("api/v1/brands", async (ISneakerService sneakerService) =>
{
    var brands = await sneakerService.GetAllBrands();
    return Results.Ok(brands);
});

app.MapGet("api/v1/occasions", async (ISneakerService sneakerService) =>
{
    var occasions = await sneakerService.GetAllOccasions();
    return Results.Ok(occasions);
});



app.Run("http://localhost:5000");
