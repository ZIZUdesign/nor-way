using ITPE3200_1_20H_nor_way.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITPE3200_1_20H_nor_way.DAL
{
    public class DBInit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<Nor_Way_Context>();

                // må slette og opprette databasen hver gang når den skalinitieres (seed`es)
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Database.EnsureCreatedAsync();

                var station1 = new Station { StationID = 1, StationName = "Oslo" };
                var station2 = new Station { StationID = 2, StationName = "Stavanger" };
                var station3 = new Station { StationID = 3, StationName = "Bergen" };
                var station4 = new Station { StationID = 4, StationName = "Trondheim" };
                var station5 = new Station { StationID = 5, StationName = "Tromsø" };
                var station6 = new Station { StationID = 6, StationName = "Bodø" };

                context.Stations.Add(station1);
                context.Stations.Add(station2);
                context.Stations.Add(station3);
                context.Stations.Add(station4);
                context.Stations.Add(station5);
                context.Stations.Add(station6);

                // lag en påoggingsbruker
                var bruker = new Admin();
                bruker.Brukernavn = "Admin";
                string passord = "Test11";
                byte[] salt = TripRepository.LagSalt();
                byte[] hash = TripRepository.LagHash(passord, salt);
                bruker.Passord = hash;
                bruker.Salt = salt;
                context.Adminer.Add(bruker);

                context.SaveChanges();
            }
        }
    }
}