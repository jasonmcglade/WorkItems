using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkItems.Core.Services;

namespace WorkItems.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkItemService _workItemService;

        public HomeController(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }

        public ActionResult Index()
        {
            ViewBag.WorkItems = _workItemService.GetAllWorkItems();

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
