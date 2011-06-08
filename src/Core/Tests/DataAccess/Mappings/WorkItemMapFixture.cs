using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using WorkItems.Core.Domain;
using FluentNHibernate.Testing;
using Core.Tests.DataAccess;

namespace WorkItems.Core.Tests.DataAccess.Mappings
{
    [TestFixture]
    [IntegrationCategory]
    public class WorkItemMapFixture : TransactionalFixtureBase
    {
        [Test]
        public void CanCorrectlyMap()
        {
            new PersistenceSpecification<WorkItem>(Session)
                .CheckProperty(c => c.Id, 1)
                .CheckProperty(c => c.Title, "Title")
                .CheckProperty(c => c.Description, "Description")
                .CheckProperty(c => c.CreatedDate, DateTime.Today)
                .CheckProperty(c => c.User, "user_name")
                .CheckProperty(c => c.Version, 5)
                .VerifyTheMappings();
        }

        [Test]
        public void SaveShouldIncrementVersion()
        {
            var entity = Save(new WorkItem());

            Assert.That(entity.Version, Is.EqualTo(1));

            Save(entity);

            Assert.That(entity.Version, Is.EqualTo(2));
        }

        [Test]
        public void CanAddCommentToExistingWorkItem()
        {
            var entity = Save(new WorkItem());

            Assert.That(entity.Comments, Is.Empty);

            entity.AddComment("some comment text", "user_name");

            Save(entity);

            var loadedEntity = Get<WorkItem>(entity.Id);

            Assert.That(loadedEntity.Comments, Is.Not.Empty);
        }
    }
}
