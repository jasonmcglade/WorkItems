﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using WorkItems.Web;
using WorkItems.Web.Controllers;
using NUnit.Framework;
using WorkItems.Core.Services;
using Rhino.Mocks;
using WorkItems.Core.Domain;

namespace Web.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerFixture
    {
        private HomeController _controller;
        private IWorkItemService _workItemService;


        [SetUp]
        public void SetUp()
        {
            _workItemService = MockRepository.GenerateMock<IWorkItemService>();
            _controller = new HomeController(_workItemService);
        }

        [Test]
        public void IndexShouldGetWorkItemsFromService()
        {
            var searchCriteria = new WorkItemSearchCriteria();
            var workItems = new WorkItems.Core.Domain.WorkItem[] { new WorkItems.Core.Domain.WorkItem() };
            _workItemService.Expect(x => x.GetWorkItemsByCriteria(searchCriteria)).Return(new WorkItemSearchResult { WorkItems = workItems });

            var result = _controller.Index(searchCriteria) as ViewResult;

            Assert.That(result.ViewBag.WorkItems, Is.EqualTo(workItems));
            _workItemService.VerifyAllExpectations();
        }

        [Test]
        public void About()
        {
            var result = _controller.About() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
