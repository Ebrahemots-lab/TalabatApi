using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;

public static class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
{
    //1- database 
    //2 - specs
    public static IQueryable<TEntity> MakeQuery(IQueryable<TEntity> inputData, ISepcifications<TEntity> specs)

    {
        var query = inputData;

        if (specs.Criateria is not null)
        {
            query = query.Where(specs.Criateria);
        }


        if (specs.OrderByAsc is null)
        {
            if (specs.OrderByDesc is not null)
            {
                query = query.OrderByDescending(specs.OrderByDesc);
            }
        }
        else
        {
            query.OrderBy(specs.OrderByAsc);
        }

        query = specs.Includes.Aggregate(query, (oldQuery, NewQuery) => oldQuery.Include(NewQuery));



        if (specs.EnablePagenation)
        {
            query = query.Skip(specs.Skip).Take(specs.Take);
        }


        return query;

    }


}