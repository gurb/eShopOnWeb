using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.PublicApi.OrderEndpoints;
using Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;

/// <summary>
/// Updates a Catalog Item
/// </summary>
public class ChangeOrderStateEndpoint : IEndpoint<IResult, ChangeOrderStateRequest, IRepository<Order>>
{ 
    private readonly IUriComposer _uriComposer;

    public ChangeOrderStateEndpoint(IUriComposer uriComposer)
    {
        _uriComposer = uriComposer;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/order/change-state",
            [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
            (ChangeOrderStateRequest request, IRepository<Order> itemRepository) =>
            {
                return await HandleAsync(request, itemRepository);
            })
            .Produces<ChangeOrderStateResponse>()
            .WithTags("OrderEndpoints");
    }

    public async Task<IResult> HandleAsync(ChangeOrderStateRequest request, IRepository<Order> itemRepository)
    {
        var response = new ChangeOrderStateResponse(request.CorrelationId());

        var existingItem = await itemRepository.GetByIdAsync(request.Id);
        if (existingItem == null)
        {
            return Results.NotFound();
        }

        existingItem.OrderState = (OrderState)request.OrderState;


        await itemRepository.UpdateAsync(existingItem);

        var dto = new OrderDto
        {
            Id = existingItem.Id,
            BuyerId = existingItem.BuyerId,
            OrderDate = existingItem.OrderDate,
            OrderState = (BlazorShared.Models.OrderState)existingItem.OrderState,
            ShipToAddress = new ShipToAddressDto
            {
                City = existingItem.ShipToAddress.City,
                State = existingItem.ShipToAddress.State,
                Street = existingItem.ShipToAddress.Street,
                Country = existingItem.ShipToAddress.Country,
                ZipCode = existingItem.ShipToAddress.ZipCode,
            }
        };
        response.Order = dto;
        return Results.Ok(response);
    }
}
