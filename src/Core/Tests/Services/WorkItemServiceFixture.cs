using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Core.Tests.DataAccess;
using WorkItems.Core.Services;
using WorkItems.Core.Domain;
using WorkItems.Core.Tests;

namespace WorkItems.Core.Tests.Services
{
    [TestFixture]
    [IntegrationCategory]
    public class WorkItemServiceFixture : TransactionalFixtureBase
    {
        private WorkItemService _service;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _service = new WorkItemService(Session);
        }

        [Test]
        public void GetAllWorkItemsWithNoItemsShouldReturnEmptyList()
        {
            var items = _service.GetWorkItemsByCriteria(new WorkItemSearchCriteria());

            Assert.That(items.WorkItems, Is.Empty);
            Assert.That(items.TotalWorkItemCount, Is.EqualTo(0));
        }

        [Test]
        public void GetAllWorkItemsShouldReturnEachSavedItem()
        {
            Save(new WorkItem { Title = "Title One" });
            Save(new WorkItem { Title = "Title Two" });

            var items = _service.GetWorkItemsByCriteria(new WorkItemSearchCriteria());

            Assert.That(items.WorkItems.Count(), Is.EqualTo(2));
        }
    }
}
