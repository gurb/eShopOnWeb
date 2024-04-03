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
    public int CatalogItemId { get; set; }
    public string ProductName { get; set; }
    public string ProductPictureUri { get; set; }
    public double UnitPrice { get; set; }
    public int Units { get; set; }
    public int OrderId { get; set; }

}
