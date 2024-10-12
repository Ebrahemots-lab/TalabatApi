using System.ComponentModel.DataAnnotations.Schema;

public class Product : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string PictureUrl { get; set; }

    public Decimal Price { get; set; }

    [ForeignKey("Brand")]
    public int BrandId { get; set; }

    [ForeignKey("Type")]
    public int TypeId { get; set; }
    public ProductType Type { get; set; }

    public Brand Brand { get; set; }

}