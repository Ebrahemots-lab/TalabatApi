public class PagenationResponse<T>
{


    public int? PageSize { get; set; }

    public int? PageIndex { get; set; }

    public int Count { get; set; }

    public IEnumerable<T>? Items { get; set; }

    public PagenationResponse(int? pageSize, int? pageIndex, IEnumerable<T> items, int count)
    {
        PageSize = pageSize;
        PageIndex = pageIndex;
        Items = items;
        Count = count;
    }


}