using System;
using BlazorShared.Models;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;


public class OrderDto
{
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public OrderState OrderState { get; set; }
    public ShipToAddressDto ShipToAddress { get; set; }
}

public class ShipToAddressDto
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
}

