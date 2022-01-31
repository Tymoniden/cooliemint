using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebControlCenter.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class VersionController : Controller
    {
        public JsonResult Get(string name)
        {
            var requestedVersion = new Version(name);
            var currentrlyInstalled = Assembly.GetExecutingAssembly().GetName().Version; ;

            return Json(new VersionModel
            {
                IsNewer = currentrlyInstalled < requestedVersion,
                IsUpgradeableTo = currentrlyInstalled == requestedVersion,
                CurrentlyInstalled = currentrlyInstalled.ToString()
            });
        }
    }

    public class VersionModel
    {
        public bool IsNewer { get; set; }

        public string CurrentlyInstalled { get; set; }

        public bool IsUpgradeableTo { get; set; }
    }
}
