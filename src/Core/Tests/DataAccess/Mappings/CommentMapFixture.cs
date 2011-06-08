using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Tests.DataAccess;
using NUnit.Framework;
using Core.Tests;
using WorkItems.Core.Domain;
using FluentNHibernate.Testing;

namespace WorkItems.Core.Tests.DataAccess.Mappings
{
    [TestFixture]
    [IntegrationCategory]
    public class CommentMapFixture : TransactionalFixtureBase
    {
        [Test]
        public void CanCorrectlyMap()
        {
            new PersistenceSpecification<Comment>(Session)
                .CheckProperty(c => c.Id, 1)
                .CheckProperty(c => c.WorkItem, new WorkItem())
                .CheckProperty(c => c.Text, "Title")
                .CheckProperty(c => c.AddedDate, DateTime.Today)
                .CheckProperty(c => c.User, "user_name")
                .CheckProperty(c => c.Version, 3)
                .VerifyTheMappings();
        }

    }
}
