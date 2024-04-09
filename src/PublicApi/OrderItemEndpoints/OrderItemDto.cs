namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

public class OrderItemDto
{
    public ItemOrderedDto ItemOrdered { get; set; }
    public int Id { get; set; }
    public decimal UnitPrice { get; set; }
    public int Units { get; set; }
    public int OrderId { get; set; }

}

public class ItemOrderedDto
{
    public string PictureUri { get; set; }
    public int CatalogItemId { get; set; }
    public string ProductName { get; set; }

}
