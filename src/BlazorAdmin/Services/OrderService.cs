using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAdmin.Services;

public class OrderService
{
    private readonly HttpService _httpService;
    private readonly ILogger<OrderService> _logger;

    public OrderService(HttpService httpService,
        ILogger<OrderService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }


    public async Task<List<BlazorShared.Models.Order>> List()
    {

        _logger.LogInformation("Fetching order items from API.");

       
        var orderListTask = _httpService.HttpGet<PagedOrderResponse>($"orders");
        await Task.WhenAll(orderListTask);
        var items = orderListTask.Result.Orders;
        
        return items;
    }

    public async Task<BlazorShared.Models.Order> ChangeOrderState(ChangeOrderStateRequest request)
    {
        var response = await _httpService.HttpPost<ChangeOrderStateResponse>("order/change-state", request);
        return response?.Order;
    }


}
