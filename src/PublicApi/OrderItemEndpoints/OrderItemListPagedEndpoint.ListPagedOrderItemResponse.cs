using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using System.Collections.Generic;
using System;

namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

public class ListPagedOrderItemResponse: BaseResponse
{
    public ListPagedOrderItemResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListPagedOrderItemResponse()
    {
    }

    public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    public int PageCount { get; set; }
}
