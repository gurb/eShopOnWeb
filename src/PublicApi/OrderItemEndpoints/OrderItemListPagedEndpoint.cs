using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;

/// <summary>
/// List Catalog Items (paged)
/// </summary>
public class OrderItemListPagedEndpoint : IEndpoint<IResult, ListPagedOrderItemRequest, IRepository<OrderItem>>
{
    private readonly IUriComposer _uriComposer;
    private readonly IMapper _mapper;

    public OrderItemListPagedEndpoint(IUriComposer uriComposer, IMapper mapper)
    {
        _uriComposer = uriComposer;
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/order-items",
            async (int? pageSize, int? pageIndex, IRepository<OrderItem> itemRepository) =>
            {
                return await HandleAsync(new ListPagedOrderItemRequest(pageSize, pageIndex), itemRepository);
            })
            .Produces<ListPagedOrderItemResponse>()
            .WithTags("OrderItemEndpoints");
    }

    public async Task<IResult> HandleAsync(ListPagedOrderItemRequest request, IRepository<OrderItem> itemRepository)
    {
        await Task.Delay(1000);
        var response = new ListPagedOrderItemResponse(request.CorrelationId());

       
        int totalItems = await itemRepository.CountAsync();

        var pagedSpec = new OrderItemFilterPaginatedSpecification(
            skip: request.PageIndex * request.PageSize,
            take: request.PageSize
            );

        var items = await itemRepository.ListAsync(pagedSpec);

        response.OrderItems.AddRange(items.Select(_mapper.Map<OrderItemDto>));
        foreach (OrderItemDto item in response.OrderItems)
        {
            item.PictureUri = _uriComposer.ComposePicUri(item.PictureUri);
        }

        if (request.PageSize > 0)
        {
            response.PageCount = int.Parse(Math.Ceiling((decimal)totalItems / request.PageSize).ToString());
        }
        else
        {
            response.PageCount = totalItems > 0 ? 1 : 0;
        }

        return Results.Ok(response);
    }
}
