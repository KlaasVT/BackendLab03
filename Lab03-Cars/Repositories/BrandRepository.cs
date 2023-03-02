namespace Lab03_Cars.Repositories;
public interface IBrandRepository
{
    Task<List<Brand>> GetAllBrands();
    Task<Brand> GetBrandById(string id);
    Task<Brand> UpdateBrand(Brand brand);
    Task<Brand> AddBrand(Brand brand);
    Task DeleteBrand(string id);
}

public class BrandRepository : IBrandRepository
{
    private readonly IMongoContext _context;
    public BrandRepository(IMongoContext context)
    {
        _context = context;
    }
    public async Task<Brand> AddBrand(Brand brand)
    {
        brand.CreatedOn = DateTime.Now;
        await _context.BrandsCollection.InsertOneAsync(brand);
        return brand;
    }

    public async Task DeleteBrand(string id)
    {
        await _context.BrandsCollection.DeleteOneAsync(brand => brand.Id == id);
    }

    public async Task<List<Brand>> GetAllBrands()
    {
        return await _context.BrandsCollection.Find(brand => true).ToListAsync();
    }

    public Task<Brand> GetBrandById(string id)
    {
        return _context.BrandsCollection.Find<Brand>(brand => brand.Id == id).FirstOrDefaultAsync();
    }

    public Task<Brand> UpdateBrand(Brand brand)
    {
        return _context.BrandsCollection.FindOneAndReplaceAsync(b => b.Id == brand.Id, brand);
    }

}