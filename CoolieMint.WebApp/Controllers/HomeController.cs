using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebControlCenter.Models;
using WebControlCenter.Services;
using WebControlCenter.Services.Database;

namespace WebControlCenter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUiConfigurationService _uiConfigurationService;
        private readonly IControlModelService _controlModelService;
        private readonly INotificationService _notificationService;

        public HomeController(IUiConfigurationService uiConfigurationService, IControlModelService controlModelService, INotificationService notificationService)
        {
            _uiConfigurationService = uiConfigurationService ?? throw new ArgumentNullException(nameof(uiConfigurationService));
            _controlModelService = controlModelService ?? throw new ArgumentNullException(nameof(controlModelService));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        public IActionResult Index()
        {
            if (HttpContext.Request.Cookies.ContainsKey("userName"))
            {
                return RedirectToAction("CurrentUser", new { userName = HttpContext.Request.Cookies["userName"] });
            }

            ViewData["users"] = _uiConfigurationService.GetConfiguredUsers();
            
            return View();
        }

        public IActionResult Home()
        {
            if (HttpContext.Request.Cookies.ContainsKey("userName"))
            {
                Response.Cookies.Delete("userName");
            }

            return RedirectToAction("Index");
        }

        public IActionResult CurrentUser(string userName, string categoryName)
        {
            if (HttpContext.Request.Cookies.ContainsKey("userName") && HttpContext.Request.Cookies["userName"] != userName)
            {
                Response.Cookies.Delete("userName");
            }

            Response.Cookies.Append("userName", userName, new Microsoft.AspNetCore.Http.CookieOptions()
            {
                Expires = DateTime.Now.AddYears(1)
            });

            var rootConfig = _uiConfigurationService.GetConfiguration(userName);
            ViewData["Title"] = userName;
            var controlModels = new List<IControlModel>();

            var currentCategory = rootConfig.Categories.FirstOrDefault(cat => cat.Name == categoryName) ??
                                  rootConfig.Categories.First();

            foreach (var controlModel in currentCategory.ControlModels)
            {
                var model = _controlModelService.Convert(controlModel);
                if (model != null)
                {
                    controlModels.Add(model);
                }
            }

            ViewData["Notifications"] = _notificationService.GetSystemNotifications();
            ViewData["CategoryName"] = currentCategory.Name;
            ViewData["Controls"] = controlModels;
            ViewData["UiCategories"] = rootConfig.Categories ?? Array.Empty<UiConfigurationCategory>();
            ViewData["UiConfigRoot"] = rootConfig;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
