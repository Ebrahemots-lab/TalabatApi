public class ProductDTO
{
    public int Id {get;set;}
     public string Name { get; set; }

    public string Description  { get; set; }

    public string PictureUrl { get; set; }

    public Decimal Price { get; set; }

    public int BrandId { get; set; }
    public int TypeId { get; set; }
    
    public string TypeName { get; set; }

    public string BrandName { get; set; }
}