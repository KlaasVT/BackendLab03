var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration.GetSection("DatabaseSettings");
builder.Services.Configure<DatabaseSettings>(configuration);
builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<ICarRepository, CarRepository>();
builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<ICarService, CarService>();
builder.Services.AddGraphQLServer()
    .AddQueryType<Queries>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
    .AddMutationType<Mutation>();
var app = builder.Build();
app.MapGraphQL();

app.MapGet("/", () => "Hello World!");
app.MapGet("/setup", async (ICarService carService) =>
{
    await carService.SetupDummyData();
    return "Setup done";
});

app.MapGet("/cars", async (ICarService carService) => await carService.GetAllCars());

app.MapGet("/cars/{id}", async (string id, ICarService carService) => await carService.GetCarById(id));

app.MapPost("/cars", async (Car car, ICarService carService) =>
{
    carService.AddCar(car);
    return Results.Created($"/cars/{car.Id}", car);
});

app.MapGet("/brands", async (ICarService carService) => await carService.GetAllBrands());

app.MapGet("/brands/{id}", async (string id, ICarService carService) =>
{
    return await carService.GetBrandById(id);
});

app.MapPost("/brands", async (Brand brand, ICarService carService) =>
{
    carService.AddBrand(brand);
    return Results.Created($"/brands/{brand.Id}", brand);
});

app.MapPut("/brands", async (Brand brand, ICarService carService) =>
{
    carService.UpdateBrand(brand);
    return Results.Ok();
});

app.MapGet("/cars/brand/{brandId}", async (string brandId, ICarService carService) => await carService.GetCarsByBrandId(brandId));

app.Run("http://localhost:5000");
