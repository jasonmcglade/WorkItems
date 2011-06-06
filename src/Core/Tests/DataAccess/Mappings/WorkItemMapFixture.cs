using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using WorkItems.Core.Domain;
using FluentNHibernate.Testing;

namespace Core.Tests.DataAccess.Mappings
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
                .VerifyTheMappings();
        }
    }
}
