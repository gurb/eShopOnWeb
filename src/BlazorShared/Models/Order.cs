using System;

namespace BlazorShared.Models;

public class Order
{
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public ShipToAddress ShipToAddress { get; set; }
    public OrderState OrderState { get; private set; }
    public double Price { get; set; }
}

public class ShipToAddress
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
}

public enum OrderState
{
    Pending,
    Approved,
    Rejected
}
