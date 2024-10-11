//this inteface will hold Criateria and inclides for our application
using System.Linq.Expressions;

public interface ISepcifications<T>
{
    //Expression that take Product and return Bool 
    public Expression<Func<T, bool>> Criateria { get; set; }

    public List<Expression<Func<T, object>>> Includes { get; set; }

    public Expression<Func<T, object>> OrderByAsc { get; set; }

    public Expression<Func<T, object>> OrderByDesc { get; set; }
}