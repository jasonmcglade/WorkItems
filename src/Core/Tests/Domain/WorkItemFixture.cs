using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using WorkItems.Core.Domain;

namespace Core.Tests.Domain
{
    [TestFixture]
    public class WorkItemFixture
    {
        [Test]
        public void CanCreateWithDefaultConstructor()
        {
            var entity = new WorkItem();

            Assert.That(entity, Is.Not.Null);
        }

        [Test]
        public void CreateShouldInitializeComments()
        {
            var entity = new WorkItem();

            Assert.That(entity.Comments, Is.Empty);
        }

    }
}
