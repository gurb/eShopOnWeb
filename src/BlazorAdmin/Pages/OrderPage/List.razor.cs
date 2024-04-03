using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorAdmin.Helpers;
using BlazorShared.Interfaces;
using BlazorShared.Models;

namespace BlazorAdmin.Pages.OrderPage;

public partial class List : BlazorComponent
{
    [Microsoft.AspNetCore.Components.Inject]
    public IOrderItemService OrderItemService { get; set; }


    private List<OrderItem> orderItems = new List<OrderItem>();

    //private Edit EditComponent { get; set; }
    //private Delete DeleteComponent { get; set; }
    //private Details DetailsComponent { get; set; }
    //private Create CreateComponent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            orderItems = await OrderItemService.List();

            CallRequestRefresh();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    //private async void DetailsClick(int id)
    //{
    //    await DetailsComponent.Open(id);
    //}

    //private async Task CreateClick()
    //{
    //    await CreateComponent.Open();
    //}

    //private async Task EditClick(int id)
    //{
    //    await EditComponent.Open(id);
    //}

    //private async Task DeleteClick(int id)
    //{
    //    await DeleteComponent.Open(id);
    //}

    private async Task ReloadCatalogItems()
    {
        orderItems = await OrderItemService.List();
        StateHasChanged();
    }
}
