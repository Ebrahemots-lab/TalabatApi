public class QueryParams
{
    //string sort, int? brandId, int? typeId
    public string sort { get; set; } = "id";

    public int? BrandIdParam { get; set; }

    public int? TypeIdParam { get; set; }

    private int pageSize = 5;
    public int PageSize //set the page size
    {
        get { return pageSize; }
        set { pageSize = value > 10 ? 5 : value; }
    }

    public int PageNumber { get; set; } = 1;

    public string? Brand { get; set; }



}
