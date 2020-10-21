using ITPE3200_1_20H_nor_way.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITPE3200_1_20H_nor_way.DAL
{
    public interface IOrderTicketRepository
    {
        Task<List<OrderTicketVM>> getAll();
        Task<bool> settInn(OrderTicketVM orderTicketVM);
        Task<List<StationVM>> getStations();
        Task<List<TripVM>> FindTrip(OrderTicketVM orderTicketVM);
        Task<TripVM> getAtrip(int id);
        Task<OrderTicketVM> getPrice(OrderTicketVM orderTicketVM);
    }
}