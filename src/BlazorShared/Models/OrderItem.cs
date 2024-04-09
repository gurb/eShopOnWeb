using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShared.Models;
public class OrderItem
{
    public int Id { get; set; }
    // sipariş edilen catalog
    public ItemOrderedDto ItemOrdered { get; set; }
    public double UnitPrice { get; set; }
    public int Units { get; set; }
    public int OrderId { get; set; }
    public OrderDTO OrderDTO { get; set; }
}

public class OrderDTO
{
    public int OrderId { get; set; }
    public string By { get; set; }
    public DateTimeOffset OrderDate { get; set; }

}

public class ItemOrderedDto
{
    public string PictureUri { get; set; }
    public int CatalogItemId { get; set; }
    public string ProductName { get; set; }

}


