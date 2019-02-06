using Clockwork.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Clockwork.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";

            ReadOnlyCollection<TimeZoneInfo> timeZones = TimeZoneInfo.GetSystemTimeZones();
            var data = new List<TimeZoneModel> { new TimeZoneModel { Id = " ", DisplayName = ""  } };
            data.AddRange(timeZones
                .Select(tz => new TimeZoneModel
                {
                    Id = tz.Id,
                    DisplayName = tz.DisplayName
                }).ToList());

            return View(data);
        }
    }
}
