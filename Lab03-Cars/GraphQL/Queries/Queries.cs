namespace Lab03_Cars.GraphQL;
public class Queries
{
    public async Task<List<Car>> GetCars([Service] ICarService carService)
    {
        return await carService.GetAllCars();
    }

    public async Task<List<Brand>> GetBrands([Service] ICarService carService)
    {
        return await carService.GetAllBrands();
    }

    public async Task<List<Car>> GetCarsByBrand([Service] ICarService carService, string brandId) => await carService.GetCarsByBrandId(brandId);

}
