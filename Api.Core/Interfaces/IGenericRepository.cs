public interface IGenericRepository<T>
{
    Task<IEnumerable<T>> GetAllWithSpecs(ISepcifications<T> specs);

    Task<T> GetProductWithSepcs(ISepcifications<T> specs);

    Task<int> GetCountOfItems(ISepcifications<T> specs);


}