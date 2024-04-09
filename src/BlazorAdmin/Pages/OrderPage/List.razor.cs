using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAdmin.Helpers;
using BlazorAdmin.Pages.OrderPage;
using BlazorAdmin.Services;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace BlazorAdmin.Pages.OrderPage;

public partial class List : BlazorComponent
{
    [Microsoft.AspNetCore.Components.Inject]
    public OrderItemService OrderItemService { get; set; }

    [Microsoft.AspNetCore.Components.Inject]
    public OrderService OrderService { get; set; }


    private List<OrderItem> orderItems = new List<OrderItem>();
    private List<Order> orders = new List<Order>();

    private DetailsOrder DetailsOrderComponent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            orderItems = await OrderItemService.List();
            orders = await OrderService.List();
            SetOrderForOrderItems();
            CallRequestRefresh();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async void DetailsClick(int id)
    {
        await DetailsOrderComponent.Open(id);
    }

    private async Task ReloadOrders()
    {
        orderItems = await OrderItemService.List();
        orders = await OrderService.List();
        SetOrderForOrderItems();
        StateHasChanged();
    }

    private void SetOrderForOrderItems()
    {
        foreach (var item in orders)
        {
            var price = orderItems.Where(x => x.OrderId == item.Id).Sum(x => (x.UnitPrice * x.Units));
            item.Price = price;
        }
    }


    private string GetState(OrderState state)
    {
        if(state == OrderState.Pending)
        {
            return "Pending";
        }
        if (state == OrderState.Approved)
        {
            return "Approved";
        }
        if (state == OrderState.Rejected)
        {
            return "Rejected";
        }
        return "Unknown";
    }

}
