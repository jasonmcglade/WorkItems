using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using WorkItems.Core.Domain;

namespace WorkItems.Core.Tests.Domain
{
    [TestFixture]
    public class CommentFixture
    {
        [Test]
        public void CreateShouldSetComments()
        {
            var comment = "provided comment";

            var entity = new Comment(comment, "user");

            Assert.That(entity.Text, Is.EqualTo(comment));
        }

        [Test]
        public void CreateShouldSetUser()
        {
            var user = "comment_user_name";

            var entity = new Comment("comment", user);

            Assert.That(entity.User, Is.EqualTo(user));
        }

        [Test]
        public void CreateShouldSetAddedDateAndTimeForCommentInUtc()
        {
            var currentTime = DateTime.UtcNow;
            var entity = new Comment("comment", "user");

            Assert.That(entity.AddedDate.Subtract(currentTime), Is.LessThan(TimeSpan.FromSeconds(1)));
        }
    }
}
