
using PruebaTecnica.Shared.Persistence.Context;


namespace PruebaTecnica.Shared.Persistence.Repositories;


public class BaseRepository
{
    protected readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }
}
