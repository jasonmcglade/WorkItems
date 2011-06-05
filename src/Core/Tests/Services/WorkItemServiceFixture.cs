using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Core.Tests.DataAccess;
using WorkItems.Core.Services;
using WorkItems.Core.Domain;

namespace Core.Tests.Services
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
            var items = _service.GetAllWorkItems();

            Assert.That(items, Is.Empty);
        }

        [Test]
        public void GetAllWorkItemsShouldReturnEachSavedItem()
        {
            Save(new WorkItem { Title = "Title One" });
            Save(new WorkItem { Title = "Title Two" });

            var items = _service.GetAllWorkItems();

            Assert.That(items.Length, Is.EqualTo(2));
        }
    }
}
