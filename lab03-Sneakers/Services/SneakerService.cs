namespace Sneakers.API.Services;
public interface ISneakerService
{
    Task<List<Sneaker>> GetAllSneakers();
    Task<Sneaker> GetSneakerById(string id);
    Task<Sneaker> AddSneaker(Sneaker sneaker);
    Task<List<Brand>> GetAllBrands();
    Task<Brand> GetBrandById(string id);
    Task<Brand> AddBrand(Brand brand);
    Task<List<Occasion>> GetAllOccasions();
    Task<Occasion> GetOccasionById(string id);
    Task<Occasion> AddOccasion(Occasion occasion);
    Task SetupData();
}
public class SneakerService : ISneakerService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IOccasionRepository _occasionRepository;
    private readonly ISneakerRepository _sneakerRepository;
    public SneakerService(IBrandRepository brandRepository, IOccasionRepository occasionRepository, ISneakerRepository sneakerRepository)
    {
        _brandRepository = brandRepository;
        _occasionRepository = occasionRepository;
        _sneakerRepository = sneakerRepository;
    }
    public async Task<List<Sneaker>> GetAllSneakers()
    {
        return await _sneakerRepository.GetAllSneakers();
    }
    public async Task<Sneaker> GetSneakerById(string id)
    {
        return await _sneakerRepository.GetSneakerById(id);
    }
    public async Task<Sneaker> AddSneaker(Sneaker sneaker)
    {
        return await _sneakerRepository.AddSneaker(sneaker);
    }
    public async Task<List<Brand>> GetAllBrands()
    {
        return await _brandRepository.GetAllBrands();
    }
    public async Task<Brand> GetBrandById(string id)
    {
        return await _brandRepository.GetBrandById(id);
    }
    public async Task<Brand> AddBrand(Brand brand)
    {
        return await _brandRepository.AddBrand(brand);
    }
    public async Task<List<Occasion>> GetAllOccasions()
    {
        return await _occasionRepository.GetAllOccasions();
    }
    public async Task<Occasion> GetOccasionById(string id)
    {
        return await _occasionRepository.GetOccasionById(id);
    }
    public async Task<Occasion> AddOccasion(Occasion occasion)
    {
        return await _occasionRepository.AddOccasion(occasion);
    }

    public async Task SetupData()
    {
        try
        {
            if (!(await _brandRepository.GetAllBrands()).Any())
                await _brandRepository.AddBrands(new List<Brand>() { new Brand() { Name = "ASICS" }, new Brand() { Name = "CONVERSE" }, new Brand() { Name = "JORDAN" }, new Brand() { Name = "PUMA" } });

            if (!(await _occasionRepository.GetAllOccasions()).Any())
                await _occasionRepository.AddOccasions(new List<Occasion>() { new Occasion() { Description = "Sports" }, new Occasion() { Description = "Casual" }, new Occasion() { Description = "Skate" }, new Occasion() { Description = "Diner" } });
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}