﻿using ITPE3200_1_20H_nor_way.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ITPE3200_1_20H_nor_way.DAL
{
    public class TripRepository : ITripRepository
    {
        private readonly Nor_Way_Context _db;
        private ILogger<TripRepository> _log;

        public TripRepository(Nor_Way_Context db, ILogger<TripRepository> log)
        {
            _db = db;
            _log = log; 
        }
        public async Task<List<TripVM>> getAll()
        {
            try
            {
                List<TripVM> allTrips = await _db.Trips.Select(t => new TripVM
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
                    seniorPrice = t.SeniorPrice,
                }).ToListAsync();
                return allTrips;
            }
            catch
            {
                return null;
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

        public async Task<bool> settInn(TripVM innTrip)
        {
            var nyTrip = new Trip()
            {
                TripDate = innTrip.tripDate,
                TripTime = innTrip.tripTime,
                AdultPrice = innTrip.adultPrice,
                StudentPrice = innTrip.studentPrice,
                ChildPrice = innTrip.childPrice,
                SeniorPrice = innTrip.seniorPrice,
                Departure = innTrip.departure,
                Arrival = innTrip.arrival
            };

            try
            {
                //Deperture Condition
                var eksistererdepartureStation = _db.Stations.FirstOrDefault(s => s.StationName == innTrip.departure);
                if (eksistererdepartureStation == null)
                {
                    var nyttDeparureStation = new Station()
                    {
                        StationName = innTrip.departure
                    };
                    nyTrip.DepartureStation = nyttDeparureStation;
                }
                else
                {
                    //setting the eksising station id in This new Trip record 
                    nyTrip.DepartureStation = eksistererdepartureStation;
                }
                //Arrival Condition
                var eksistererArrivalStation = _db.Stations.FirstOrDefault(s => s.StationName == innTrip.arrival);
                if (eksistererArrivalStation == null)
                {
                    var nyttArrivalStation = new Station()
                    {
                        StationName = innTrip.arrival
                    };
                    nyTrip.ArrivalStation = nyttArrivalStation;
                }
                else
                {
                    //setting the eksising station id in This new Trip record 
                    nyTrip.ArrivalStation = eksistererArrivalStation;
                }
                //save a Trip
                _db.Trips.Add(nyTrip);
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

        public async Task<bool> slettTrip(int slettId)
        {
            try
            {
                Trip slettTrip = _db.Trips.Find(slettId);
                _db.Trips.Remove(slettTrip);
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

        public async Task<bool> update(TripVM innTrip)
        {
            Trip endreTrip = _db.Trips.Find(innTrip.id);
            endreTrip.TripDate = innTrip.tripDate;
            endreTrip.TripTime = innTrip.tripTime;
            endreTrip.Departure = innTrip.departure;
            endreTrip.Arrival = innTrip.arrival;
            endreTrip.AdultPrice = innTrip.adultPrice;
            endreTrip.StudentPrice = innTrip.studentPrice;
            endreTrip.ChildPrice = innTrip.childPrice;
            endreTrip.SeniorPrice = innTrip.seniorPrice;

            try
            {
                //Deperture Condition
                var eksistererdepartureStation = _db.Stations.FirstOrDefault(s => s.StationName == innTrip.departure);
                if (eksistererdepartureStation == null)
                {
                    var nyttDeparureStation = new Station()
                    {
                        StationName = innTrip.departure
                    };
                    endreTrip.DepartureStation = nyttDeparureStation;
                }
                else
                {
                    //setting the eksising station id in This new Trip record 
                    endreTrip.DepartureStation = eksistererdepartureStation;
                }
                //Arrival Condition
                var eksistererArrivalStation = _db.Stations.FirstOrDefault(s => s.StationName == innTrip.arrival);
                if (eksistererArrivalStation == null)
                {
                    var nyttArrivalStation = new Station()
                    {
                        StationName = innTrip.arrival
                    };
                    endreTrip.ArrivalStation = nyttArrivalStation;
                }
                else
                {
                    //setting the eksising station id in This new Trip record 
                    endreTrip.ArrivalStation = eksistererArrivalStation;
                }
                //update a Trip
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

        public static byte[] LagHash(string passord, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                                password: passord,
                                salt: salt,
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 1000,
                                numBytesRequested: 32);
        }

        public static byte[] LagSalt()
        {
            var csp = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csp.GetBytes(salt);
            return salt;
        }

        public async Task<bool> LoggInn(AdminVM bruker)
        {
            try
            {
                Admin funnetBruker = await _db.Adminer.FirstOrDefaultAsync(b => b.Brukernavn == bruker.Brukernavn);
                // sjekk passordet
                byte[] hash = LagHash(bruker.Passord, funnetBruker.Salt);
                bool ok = hash.SequenceEqual(funnetBruker.Passord);
                if (ok)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }
    }
}
