namespace Sneakers.API.Repositories;
public interface IBrandRepository
{
    Task<List<Brand>> GetAllBrands();
    Task<Brand> GetBrandById(string id);
    Task<Brand> AddBrand(Brand brand);
    Task<List<Brand>> AddBrands(List<Brand> brands);
}
public class BrandRepository : IBrandRepository
{
    private readonly IMongoContext _context;
    public BrandRepository(IMongoContext context)
    {
        _context = context;
    }
    public async Task<List<Brand>> GetAllBrands()
    {
        return await _context.BrandsCollection.Find(_ => true).ToListAsync();
    }
    public async Task<Brand> GetBrandById(string id)
    {
        return await _context.BrandsCollection.Find<Brand>(brand => brand.BrandId == id).FirstOrDefaultAsync();
    }
    public async Task<Brand> AddBrand(Brand brand)
    {
        await _context.BrandsCollection.InsertOneAsync(brand);
        return brand;
    }
    public async Task<List<Brand>> AddBrands(List<Brand> brands)
    {
        await _context.BrandsCollection.InsertManyAsync(brands);
        return brands;
    }
}