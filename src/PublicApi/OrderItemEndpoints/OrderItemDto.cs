namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

public class OrderItemDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int CatalogId { get; set; }
    public decimal Price { get; set; }
    public string PictureUri { get; set; }
    public int Units { get; set; }
    public int OrderId { get; set; }
}
