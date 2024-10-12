namespace Api.Core.Specifications
{
    public class CountSpecifications : Specifications<Product>
    {
        public CountSpecifications(QueryParams param) : base(P =>
        (!param.BrandIdParam.HasValue || P.BrandId == param.BrandIdParam)
        &&
        (!param.TypeIdParam.HasValue || P.TypeId == param.TypeIdParam)
        )
        {

        }
    }
}