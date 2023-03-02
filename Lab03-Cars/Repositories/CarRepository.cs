namespace Lab03_Cars.Repositories;
public interface ICarRepository
{
    Task<List<Car>> GetAllCars();
    Task<Car> GetCarById(string id);
    Task AddCar(Car car);
    Task<List<Car>> GetCarsByBrandId(string brandId);
}

public class CarRepository : ICarRepository
{
    private readonly IMongoContext _context;
    public CarRepository(IMongoContext context)
    {
        _context = context;
    }
    public async Task AddCar(Car car)
    {
        car.CreatedOn = DateTime.Now;
        await _context.CarsCollection.InsertOneAsync(car);
    }
    public async Task<List<Car>> GetAllCars()
    {
        return await _context.CarsCollection.Find(car => true).ToListAsync();
    }
    public Task<Car> GetCarById(string id)
    {
        return _context.CarsCollection.Find<Car>(car => car.Id == id).FirstOrDefaultAsync();
    }

    public Task<List<Car>> GetCarsByBrandId(string brandId)
    {
        return _context.CarsCollection.Find<Car>(car => car.Brand.Id == brandId).ToListAsync();
    }
}
