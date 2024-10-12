using System.Linq.Expressions;

public class ProductSpecifications : Specifications<Product>
{

    public ProductSpecifications(QueryParams param)
    : base(P =>
        (!param.BrandIdParam.HasValue || P.BrandId == param.BrandIdParam)
        &&
        (!param.TypeIdParam.HasValue || P.TypeId == param.TypeIdParam)
     )
    {
        switch (param.sort)
        {
            case "id":
                OrderByAsc = P => P.Id;
                break;
            case "nameAsc":
                OrderByAsc = P => P.Name;
                break;
            case "nameDesc":
                OrderByDesc = P => P.Name;
                break;
            case "priceAsc":
                OrderByAsc = P => P.Price;
                break;
            case "priceDesc":
                OrderByDesc = P => P.Price;
                break;
            default:
                OrderByAsc = P => P.Id;
                OrderByDesc = P => P.Id;
                break;

        }


        AddIncludes();
        //total products = 50
        //Pagesize = 10
        //skip = 10 * 2 - 1 
        //pageNumber = 2
        int skipCalculator = param.PageSize * (param.PageNumber - 1);
        ApplyPagenation(param.PageSize, skipCalculator);

    }
    public ProductSpecifications(Expression<Func<Product, bool>> criteria) : base(criteria)
    {
        AddIncludes();
    }

    public ProductSpecifications()
    {
        AddIncludes();
    }


    public void ApplyPagenation(int take, int skip)
    {
        EnablePagenation = true;
        Take = take;
        Skip = skip;
    }


    public void AddIncludes()
    {

        Includes.Add(P => P.Brand);
        Includes.Add(P => P.Type);
    }

}