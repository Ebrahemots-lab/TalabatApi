using Microsoft.EntityFrameworkCore;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ApplicationContext _context;

    //Inject application context Class to connect to data base 
    public GenericRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllWithSpecs(ISepcifications<T> specs)
    {
        return await ApplySpecifications(specs).ToListAsync();
    }

    public async Task<T> GetProductWithSepcs(ISepcifications<T> specs)
    {
        return await ApplySpecifications(specs).FirstOrDefaultAsync();
    }


    private IQueryable<T> ApplySpecifications(ISepcifications<T> specs)
    {
        return SpecificationsEvaluator<T>.MakeQuery(_context.Set<T>(), specs);
    }
}
