namespace Lab03_Cars.Services;
public interface ICarService
{
    Task<List<Brand>> GetAllBrands();
    Task<Brand> GetBrandById(string id);
    Task<Brand> AddBrand(Brand brand);
    void UpdateBrand(Brand brand);
    Task<List<Car>> GetAllCars();
    Task<Car> GetCarById(string id);
    void AddCar(Car car);
    Task SetupDummyData();
    Task<List<Car>> GetCarsByBrandId(string brandId);
}
public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IBrandRepository _brandRepository;
    public CarService(ICarRepository carRepository, IBrandRepository brandRepository)
    {
        _carRepository = carRepository;
        _brandRepository = brandRepository;
    }
    
    public Task<List<Car>> GetCarsByBrandId(string brandId)
    {
        return _carRepository.GetCarsByBrandId(brandId);
    }
    public Task<List<Brand>> GetAllBrands()
    {
        return _brandRepository.GetAllBrands();
    }

    public Task<Brand> GetBrandById(string id)
    {
        return _brandRepository.GetBrandById(id);
    }

    public Task<Brand> AddBrand(Brand brand)
    {
        return _brandRepository.AddBrand(brand);
    }

    public void UpdateBrand(Brand brand)
    {
        _brandRepository.UpdateBrand(brand);
    }

    public Task<List<Car>> GetAllCars()
    {
        return _carRepository.GetAllCars();
    }

    public Task<Car> GetCarById(string id)
    {
        return _carRepository.GetCarById(id);
    }

    public void AddCar(Car car)
    {
        _carRepository.AddCar(car);
    }
    public async Task SetupDummyData()
    {
        if (!(await _brandRepository.GetAllBrands()).Any())
        {

            var brands = new List<Brand>(){
            new Brand()
            {
            Country = "Germany" , Name = "Volkswagen"
            },
            new Brand()
            {
           Country = "Germany" , Name = "BMW"
            },
            new Brand()
            {
         Country = "Germany" , Name = "Audi"
            },
            new Brand()
            {
              Country = "USA" , Name = "Tesla"
            }
        };

            foreach (var brand in brands)
                await _brandRepository.AddBrand(brand);
        }

        if (!(await _carRepository.GetAllCars()).Any())
        {
            var brands = await _brandRepository.GetAllBrands();
            var cars = new List<Car>()
        {
            new Car(){

                Name = "ID.3",
                Brand = brands[0],
            },
            new Car(){

                Name = "ID.4",
                Brand = brands[0],
            },
            new Car(){

                Name = "IX3",
                Brand = brands[1],
            },
            new Car(){

                Name = "E-Tron",
                Brand = brands[2],
            },
            new Car(){

                Name = "Model Y",
                Brand = brands[3],
            }
        };
            foreach (var car in cars)
                await _carRepository.AddCar(car);
        }
    }

}

