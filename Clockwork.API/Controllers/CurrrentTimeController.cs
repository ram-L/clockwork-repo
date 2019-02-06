using System;
using Microsoft.AspNetCore.Mvc;
using Clockwork.API.Models;
using System.Collections.Generic;

namespace Clockwork.API.Controllers
{
    [Route("api/[controller]")]
    public class CurrentTimeController : Controller
    {
        // GET api/currenttime
        [HttpGet]
        public IActionResult Get()
        {
            using (var db = new ClockworkContext())
            {
                var returnVal = new List<CurrentTimeQuery>();
                foreach (var CurrentTimeQuery in db.CurrentTimeQueries)
                {
                    returnVal.Add(new CurrentTimeQuery
                    {
                        CurrentTimeQueryId = CurrentTimeQuery.CurrentTimeQueryId,
                        Time = CurrentTimeQuery.Time, 
                        ClientIp = CurrentTimeQuery.ClientIp,
                        UTCTime = CurrentTimeQuery.UTCTime,
                        TimeZone = CurrentTimeQuery.TimeZone
                    });
                }
                return Ok(returnVal);
            }
        }

        // GET api/currenttime/timeZoneId
        [HttpGet("{timeZoneId}")]
        public IActionResult Get(string timeZoneId)
        {    
            var utcTime = DateTime.UtcNow;
            var serverTime = DateTime.Now;
            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();
            var timeZone = "";

            if (!string.IsNullOrEmpty(timeZoneId))
            {
                try
                {
                    TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                    serverTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZoneInfo);
                    timeZone = timeZoneInfo.DisplayName;
                }
                catch (TimeZoneNotFoundException)
                {
                    return NotFound();
                }
                catch (InvalidTimeZoneException)
                {
                    return NotFound();
                }

            }

            using (var db = new ClockworkContext())
            {
                var newVal = new CurrentTimeQuery
                {
                    UTCTime = utcTime,
                    ClientIp = ip,
                    Time = serverTime,
                    TimeZone = timeZone.Trim()
                };
                db.CurrentTimeQueries.Add(newVal);
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved to database", count);

                return Ok(serverTime);
            }
        }
    }
}
