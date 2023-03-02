namespace Sneakers.API.Repositories;
public interface ISneakerRepository
{
    Task<List<Sneaker>> GetAllSneakers();
    Task<Sneaker> GetSneakerById(string id);
    Task<Sneaker> AddSneaker(Sneaker sneaker);
}
public class SneakerRepository : ISneakerRepository
{
    private readonly IMongoContext _context;
    public SneakerRepository(IMongoContext context)
    {
        _context = context;
    }
    public async Task<List<Sneaker>> GetAllSneakers()
    {
        return await _context.SneakerCollection.Find(_ => true).ToListAsync();
    }
    public async Task<Sneaker> GetSneakerById(string id)
    {
        return await _context.SneakerCollection.Find<Sneaker>(sneaker => sneaker.SneakerId == id).FirstOrDefaultAsync();
    }
    public async Task<Sneaker> AddSneaker(Sneaker sneaker)
    {
        await _context.SneakerCollection.InsertOneAsync(sneaker);
        return sneaker;
    }

}