namespace Sneakers.API.Repositories;

public interface IOccasionRepository
{
    Task<List<Occasion>> GetAllOccasions();
    Task<Occasion> GetOccasionById(string id);
    Task<Occasion> AddOccasion(Occasion occasion);
    Task<List<Occasion>> AddOccasions(List<Occasion> occasions);
}
public class OccasionRepository : IOccasionRepository
{
    private readonly IMongoContext _context;
    public OccasionRepository(IMongoContext context)
    {
        _context = context;
    }
    public async Task<List<Occasion>> GetAllOccasions()
    {
        return await _context.OccasionCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Occasion> GetOccasionById(string id)
    {
        return await _context.OccasionCollection.Find<Occasion>(occasion => occasion.OccasionId == id).FirstOrDefaultAsync();
    }

    public async Task<Occasion> AddOccasion(Occasion occasion)
    {
        await _context.OccasionCollection.InsertOneAsync(occasion);
        return occasion;
    }

    public async Task<List<Occasion>> AddOccasions(List<Occasion> occasions)
    {
        await _context.OccasionCollection.InsertManyAsync(occasions);
        return occasions;
    }
}