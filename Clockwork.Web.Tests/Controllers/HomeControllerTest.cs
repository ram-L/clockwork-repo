using NUnit.Framework;
using System;
using System.Web.Mvc;
using Clockwork.Web.Controllers;
using System.Collections.ObjectModel;

namespace Clockwork.Web.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Index()
        {
            ReadOnlyCollection<TimeZoneInfo> timeZones;
            timeZones = TimeZoneInfo.GetSystemTimeZones();
            foreach(var x in timeZones)
            {
                Console.WriteLine("{0} | {1} | {2} | {3} |", x.Id, x.DisplayName, x.DaylightName, x.StandardName);
            }

            // Arrange
            var controller = new HomeController();

            // Act
            var result = (ViewResult)controller.Index();

            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            var expectedVersion = mvcName.Version.Major + "." + mvcName.Version.Minor;
            var expectedRuntime = isMono ? "Mono" : ".NET";

            // Assert
            Assert.AreEqual(expectedVersion, result.ViewData["Version"]);
            Assert.AreEqual(expectedRuntime, result.ViewData["Runtime"]);
        }
    }
}
