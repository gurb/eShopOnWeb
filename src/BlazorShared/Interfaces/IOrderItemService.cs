using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorShared.Models;

namespace BlazorShared.Interfaces;

public interface IOrderItemService
{
    //Task<OrderItem> Create(CreateCatalogItemRequest catalogItem);
    //Task<OrderItem> Edit(CatalogItem catalogItem);
    //Task<string> Delete(int id);
    //Task<OrderItem> GetById(int id);
    Task<List<OrderItem>> ListPaged(int pageSize);
    Task<List<OrderItem>> List();
}



