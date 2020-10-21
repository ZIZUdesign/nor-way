using ITPE3200_1_20H_nor_way.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITPE3200_1_20H_nor_way.DAL
{
    public class StationRepository : IStationRepository
    {
        private readonly Nor_Way_Context _db;

        public StationRepository(Nor_Way_Context db)
        {
            _db = db;
        }
      
        public IList<Station> GetAllStations()
        {
            var query = from s in _db.Stations
                        select s;
            var content = query.ToList<Station>();
            return content;
        }
        public IList<Station> ComleteStationNameById(int stationId)
        {
            var query = from s in _db.Stations
                        where s.StationID == stationId
                        select s;
            var content = query.ToList<Station>();
            return content;
        }

        public object CompleteStationNameBy(string prefix)
        {
            var completedStationName = (from s in _db.Stations
                             where s.StationName.StartsWith(prefix)
                             select new
                             {
                                 label = s.StationName,
                                 val = s.StationID
                             }).ToList();

            return completedStationName;
        }

        public IList<Station> GetAllStationById(int stationId)
        {
            throw new NotImplementedException();
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