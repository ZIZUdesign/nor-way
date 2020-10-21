using ITPE3200_1_20H_nor_way.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ITPE3200_1_20H_nor_way.DAL
{
    public class OrderTicketRepository : IOrderTicketRepository
    {
        private readonly Nor_Way_Context _db;

        public OrderTicketRepository(Nor_Way_Context db)
        {
            _db = db;
        }

        public async Task<List<OrderTicketVM>> getAll()
        {
            try
            {
                List<OrderTicketVM> Bestillings = await _db.Bestillings.Select(b => new OrderTicketVM
                {
                    id = b.ID,
                    Date = b.Date,
                    departure = b.Departure,
                    arrival = b.Arrival,
                    numberOfAdults = b.NumberOfAdults,
                    numberOfStudents = b.NumberOfStudents,
                    numberOfKids = b.NumberOfKids,
                    numberOfSeniors = b.NumberOfSeniors
                }).ToListAsync();
                return Bestillings;
            }
            catch
            {
                return null;
            }
        }

        public async Task<OrderTicketVM> getPrice(OrderTicketVM order)
        {
            OrderTicketVM orderTicketVM = new OrderTicketVM();
            //local variables
            float adultsTotal = 0;
            float studentsTotal = 0;
            float kidsTotal = 0;
            float seniorTotal = 0;
            //float total = 0;
            //
            orderTicketVM.departure = order.departure;
            orderTicketVM.arrival = order.arrival;
            orderTicketVM.tripId = order.tripId;
            orderTicketVM.numberOfAdults = order.numberOfAdults;
            orderTicketVM.numberOfStudents = order.numberOfStudents;
            orderTicketVM.numberOfKids = order.numberOfKids;
            orderTicketVM.numberOfSeniors = order.numberOfSeniors;

            try
            {

                var selectedTrip = await _db.Trips.FirstOrDefaultAsync(s => s.TripID == order.tripId);
                if (selectedTrip == null) { return orderTicketVM; }
                else
                    adultsTotal = order.numberOfAdults * selectedTrip.AdultPrice;
                    studentsTotal = order.numberOfStudents * selectedTrip.StudentPrice;
                    kidsTotal = order.numberOfKids * selectedTrip.ChildPrice;
                    seniorTotal = order.numberOfSeniors * selectedTrip.SeniorPrice;

                orderTicketVM.totalPrice = adultsTotal + studentsTotal + kidsTotal + seniorTotal;
                orderTicketVM.tripId = selectedTrip.TripID;


                return orderTicketVM; ;
            }
            catch (IOException e)
            {
                if (e.Source != null)
                    Console.WriteLine("IOException source: {0}", e.Source);
                throw;
            }
        }

        public async Task<List<StationVM>> getStations()
        {
            try
            {
                List<StationVM> stations = await _db.Stations.Select(s => new StationVM
                {
                    stationID=s.StationID,
                    stationName=s.StationName
                }).ToListAsync();
                return stations;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> settInn(OrderTicketVM order)
        {
            var nyBestilling = new OrderTicket()
            {
                Date = DateTime.Now,
                NumberOfAdults = order.numberOfAdults,
                NumberOfStudents = order.numberOfStudents,
                NumberOfKids = order.numberOfKids,
                NumberOfSeniors = order.numberOfSeniors,
                Departure = order.departure,
                Arrival = order.arrival,
                TotalPrice = order.totalPrice,
                KontoNo = order.kontoNo,
                PinKode = order.pinKode,
                MobilNo = order.mobilNo,
                TripID = order.tripId,
                HarBetalt = true
            };

            try
            {
                var selectedTrip = await _db.Trips.FirstOrDefaultAsync(s => s.TripID == order.tripId);
                if (selectedTrip == null) { return false; }
                else
                    nyBestilling.Trip = selectedTrip;
                //save a Trip
                _db.Bestillings.Add(nyBestilling);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (IOException e)
            {
                if (e.Source != null)
                    Console.WriteLine("IOException source: {0}", e.Source);
                throw;
            }
        }

        public async Task<List<TripVM>> FindTrip(OrderTicketVM orderTicketVM)
        {
            var departureStation = await _db.Stations.FirstOrDefaultAsync(s => s.StationName == orderTicketVM.selectedDeparture);
            var arrivalStation = _db.Stations.FirstOrDefault(s => s.StationName == orderTicketVM.selectedAarrival);
            {
                try
                {
                    var query = from t in _db.Trips
                                orderby t.TripDate
                                where
                                     t.Departure == departureStation.StationName
                                  && t.Arrival == arrivalStation.StationName
                                  && t.TripDate >= orderTicketVM.selectedDate

                                select new TripVM()
                                {
                                    id = t.TripID,
                                    tripDate = t.TripDate,
                                    StringTripDate = t.TripDate.ToString(),
                                    tripTime = t.TripTime,
                                    StringTripTime = t.TripTime.ToString(),
                                    departure = t.Departure,
                                    arrival = t.Arrival,
                                    adultPrice = t.AdultPrice,
                                    studentPrice = t.StudentPrice,
                                    childPrice = t.ChildPrice,
                                    seniorPrice = t.SeniorPrice

                                };

                    return query.ToList();
                }
                catch (IOException e)
                {
                    if (e.Source != null)
                        Console.WriteLine("IOException source: {0}", e.Source);
                    throw;

                }
                finally
                {
                    _db.Dispose();
                }
                //return null;
            }
        }

        public async Task<TripVM> getAtrip(int id)
        {
            Trip dbTrip = await _db.Trips.FindAsync(id);
            if (dbTrip == null)
            {
                return null;
            }
            else
            {
                var utTrip = new TripVM()
                {
                    id = dbTrip.TripID,
                    tripDate = dbTrip.TripDate,
                    StringTripDate = dbTrip.TripDate.ToString("MM/dd/yyyy"),
                    tripTime = dbTrip.TripTime,
                    StringTripTime = dbTrip.TripTime.ToString("MM/dd/yyyy"),
                    departure = dbTrip.DepartureStation.StationName,
                    arrival = dbTrip.ArrivalStation.StationName,
                    adultPrice = dbTrip.AdultPrice,
                    studentPrice = dbTrip.StudentPrice,
                    childPrice = dbTrip.ChildPrice,
                    seniorPrice = dbTrip.SeniorPrice


                };
                return utTrip;
            }
        }
    }
}
