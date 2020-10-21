using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITPE3200_1_20H_nor_way.DAL;
using ITPE3200_1_20H_nor_way.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITPE3200_1_20H_nor_way.Controllers
{
    [Route("[controller]/[action]")]
    public class TripController : ControllerBase
    {
        private readonly ITripRepository _db;
        private ILogger<TripController> _log;

        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";

        public TripController(ITripRepository db, ILogger<TripController> log)
        {
            _db = db;
            _log = log; 
        }
        public async Task<ActionResult> Save(TripVM tripVM)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.settInn(tripVM);
                if (!returOK)
                {
                    _log.LogInformation("Turen kunne ikke lagres!");
                    return BadRequest("Turen kunne ikke lagres");
                }
                return Ok("turen lagret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
            
        }
        public async Task<ActionResult> GetAll()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            List<TripVM> alletur = await _db.getAll();
            return Ok(alletur);
            
        }
        public async Task<ActionResult> RemoveBiId(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            bool returOK = await _db.slettTrip(id);
            if (!returOK)
            {
                _log.LogInformation("Sletting av turen ble ikke utført");
                return NotFound("Sletting av turen ble ikke utført");
            }
            return Ok("turen slettet");
        }
        public async Task<ActionResult> GetAtrip(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            TripVM turen = await _db.getAtrip(id);
            if (turen == null)
            {
                _log.LogInformation("Fant ikke kunden");
                return NotFound("Fant ikke kunden");
            }
            return Ok(turen);
        }
        public async Task<ActionResult> Edit(TripVM tripVM)
        {
            
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.update(tripVM);
                if (!returOK)
                {
                    _log.LogInformation("Endringen kunne ikke utføres");
                    return NotFound("Endringen av turen kunne ikke utføres");
                }
                return Ok("turen endret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> LoggInn(AdminVM bruker)
        {
            if (ModelState.IsValid)
            {
                bool returnOK = await _db.LoggInn(bruker);
                if (!returnOK)
                {
                    _log.LogInformation("Innloggingen feilet for bruker");
                    HttpContext.Session.SetString(_loggetInn, _ikkeLoggetInn);
                    return Ok(false);
                }
                HttpContext.Session.SetString(_loggetInn, _loggetInn);
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public void LoggUt()
        {
            HttpContext.Session.SetString(_loggetInn, _ikkeLoggetInn);
        }
    }
}
