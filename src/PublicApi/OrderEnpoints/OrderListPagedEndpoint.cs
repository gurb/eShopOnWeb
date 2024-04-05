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
public class OrderListPagedEndpoint : IEndpoint<IResult, ListPagedOrderRequest, IRepository<Order>>
{
    private readonly IUriComposer _uriComposer;
    private readonly IMapper _mapper;

    public OrderListPagedEndpoint(IUriComposer uriComposer, IMapper mapper)
    {
        _uriComposer = uriComposer;
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/orders",
            async (int? pageSize, int? pageIndex, IRepository<Order> itemRepository) =>
            {
                return await HandleAsync(new ListPagedOrderRequest(pageSize, pageIndex), itemRepository);
            })
            .Produces<ListPagedOrderResponse>()
            .WithTags("OrderEndpoints");
    }

    public async Task<IResult> HandleAsync(ListPagedOrderRequest request, IRepository<Order> itemRepository)
    {
        var response = new ListPagedOrderResponse(request.CorrelationId());

       
        int totalItems = await itemRepository.CountAsync();

        
        var items = await itemRepository.ListAsync();

        response.Orders.AddRange(items.Select(_mapper.Map<OrderDto>));

        

        return Results.Ok(response);
    }
}
