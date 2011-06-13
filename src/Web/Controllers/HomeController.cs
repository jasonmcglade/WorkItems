using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkItems.Core.Services;
using WorkItems.Core.Domain;

namespace WorkItems.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkItemService _workItemService;

        public HomeController(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }

        public ActionResult Index(WorkItemSearchCriteria criteria)
        {
            var searchResult = _workItemService.GetWorkItemsByCriteria(criteria);

            ViewBag.PageSize = WorkItemSearchCriteria.PageSize;
            ViewBag.WorkItems = searchResult.WorkItems;
            ViewBag.WorkItemCount = searchResult.TotalWorkItemCount;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
