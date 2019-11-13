using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Supplementler.Model;
using Supplementler.Web.Models;
using Supplementler.Web.Providers;

namespace Supplementler.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPluginProvider provider;
        private readonly ILogger<HomeController> log;
        public HomeController(IPluginProvider _provider, ILogger<HomeController> _log)
        {
            provider = _provider;
            log = _log;
        }
        public IActionResult Index()
        {
            log.LogInformation("Hello, world!");
            log.LogInformation("Indexx");
            /*
             TO DO :

            Repository Pattern entegre edilecek ama nerede kullanılacak.
            Projeyi github a private şekliyle at.
            2.kez commit ver.
             */
            //Loglamalar
            var allPluginDatas = provider.GetAllDatasFromPlugins()
                                .Select(s => new PluginItem() {  PluginId = s.PluginId, PluginValue = s.PluginValue })
                                .ToList();

            List<SupplementlerMenuModel> menuItems = GetMenuItems(allPluginDatas);
            List<SupplementlerWidgetModel> widgetItems = GetWidgetItems(allPluginDatas);

            var model = new IndexViewModel()
            {
                MenuList = menuItems,
                WidgetList = widgetItems
            };

            return View(model);
        }

        public IActionResult About()
        {
            log.LogInformation("About Logs");
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            log.LogInformation("Contact Logs");
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            log.LogInformation("Privacy Logs");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            log.LogInformation("Hello, world!");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<SupplementlerWidgetModel> GetWidgetItems(List<PluginItem> allPluginDatas)
        {
            log.LogInformation("Widgets are coming!");
            string searchKey = ".Widget";
            List<SupplementlerWidgetModel> widgetItems = new List<SupplementlerWidgetModel>();
            var widgetPluginDatas = allPluginDatas.FirstOrDefault(x => x.PluginId.Contains(searchKey))?.PluginValue;
            if (!string.IsNullOrEmpty(widgetPluginDatas))
            {
                widgetItems = JsonConvert.DeserializeObject<List<SupplementlerWidgetModel>>(widgetPluginDatas);
            }

            return widgetItems;
        }

        private List<SupplementlerMenuModel> GetMenuItems(List<PluginItem> allPluginDatas)
        {
            log.LogInformation("Menus are coming!");
            string searchKey = ".Menu";
            List<SupplementlerMenuModel> menuItems = new List<SupplementlerMenuModel>();
            var menuPluginDatas = allPluginDatas.FirstOrDefault(x => x.PluginId.Contains(searchKey))?.PluginValue;
            if (!string.IsNullOrEmpty(menuPluginDatas))
            {
                menuItems = JsonConvert.DeserializeObject<List<SupplementlerMenuModel>>(menuPluginDatas);
            }

            return menuItems;
        }
    }
}
