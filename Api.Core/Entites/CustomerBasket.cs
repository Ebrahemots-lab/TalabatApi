namespace Api.Core.Entites;

public class CustomerBasket
{
    public string Id { get; set; }

    public List<BasketItem> Items { get; set; }


    public CustomerBasket(string id)
    {
        Id = id;
    }
}
