using System.Linq.Expressions;

public class Specifications<T> : ISepcifications<T>
{
    public Expression<Func<T, bool>> Criateria { get; set; }
    public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
    public Expression<Func<T, object>> OrderByAsc { get; set; } //Default
    public Expression<Func<T, object>> OrderByDesc { get; set; }  // Default
    public int Take { get; set; }
    public int Skip { get; set; }

    public Specifications()
    {

    }

    public Specifications(Expression<Func<T, bool>> criateria)
    {
        Criateria = criateria;

    }

}