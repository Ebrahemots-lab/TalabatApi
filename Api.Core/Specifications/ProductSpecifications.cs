using System.Linq.Expressions;

public class ProductSpecifications : Specifications<Product>
{

    public ProductSpecifications(string type, int? brandIdParameter, int? typeId)
    : base(P =>
        (!brandIdParameter.HasValue || P.BrandId == brandIdParameter)
        &&
        (typeId.HasValue || P.TypeId == typeId)
     )
    {
        switch (type)
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
    }
    public ProductSpecifications(Expression<Func<Product, bool>> criteria) : base(criteria)
    {
        Includes.Add(P => P.Brand);
        Includes.Add(P => P.Type);
    }

    public ProductSpecifications()
    {
        Includes.Add(P => P.Brand);
        Includes.Add(P => P.Type);
    }




}