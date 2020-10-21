using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Logging;
using ITPE3200_1_20H_nor_way.DAL;
using ITPE3200_1_20H_nor_way.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITPE3200_1_20H_nor_way.Controllers
{
    [Route("[controller]/[action]")]
    public class OrderTicketController : Controller
    {
        private readonly IOrderTicketRepository _db;
        private ILogger<OrderTicketController> _log;

        public OrderTicketController(IOrderTicketRepository db, ILogger<OrderTicketController> log)
        {
            _db = db;
            _log = log;
        }
        public async Task<List<StationVM>> GetStations()
        {
            return await _db.getStations();
        }
        public async Task<List<TripVM>> FindTrip(OrderTicketVM orderTicketVM)
        {
            Console.WriteLine(orderTicketVM.selectedDeparture);
            Console.WriteLine(orderTicketVM.selectedAarrival);
            return await _db.FindTrip(orderTicketVM);
        }
         public async Task<TripVM> GetAtrip(int id)
        {
            return await _db.getAtrip(id);
        }
        public async Task<OrderTicketVM> GetPrice(OrderTicketVM orderTicketVM)
        {
            return await _db.getPrice(orderTicketVM);
        }
        public async Task<ActionResult> Save(OrderTicketVM orderTicketVM)
        {
            bool returnOK = await _db.settInn(orderTicketVM);
            if(!returnOK)
            {
                _log.LogInformation("FEILMELDING HER!");
                return BadRequest("FEILMELDNG HER!");
            }
            return Ok("OPERASJON VELLYKKET HER!");
        }
    }
}
