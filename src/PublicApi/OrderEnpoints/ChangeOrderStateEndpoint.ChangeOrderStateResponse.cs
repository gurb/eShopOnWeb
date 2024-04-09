using System;
using Microsoft.eShopWeb.PublicApi.OrderEndpoints;

namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;

public class ChangeOrderStateResponse : BaseResponse
{
    public ChangeOrderStateResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ChangeOrderStateResponse()
    {
    }

    public OrderDto Order { get; set; }
}
