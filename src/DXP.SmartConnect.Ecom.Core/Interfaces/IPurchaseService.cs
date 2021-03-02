using DXP.SmartConnect.Ecom.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.Core.Interfaces
{
    public interface IPurchaseService
    {
        Task<ItemsIBuyDto> GetItemsIBuyLast90Days(int limit, int dayOffset, int storeId);
    }
}
