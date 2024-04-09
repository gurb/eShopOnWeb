using System.ComponentModel.DataAnnotations;
using BlazorShared.Models;

namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;

public class ChangeOrderStateRequest : BaseRequest
{
    [Range(1, 10000)]
    public int Id { get; set; }
    
    public OrderState OrderState { get; set; }
}
