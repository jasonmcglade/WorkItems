﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using WorkItems.Core.Domain;

namespace WorkItems.Core.Tests.Domain
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
        public void CreateShouldDefaultTheCreateDate()
        {
            var currentTime = DateTime.UtcNow;
            var entity = new WorkItem();

            Assert.That(entity.CreatedDate.Subtract(currentTime), Is.LessThan(TimeSpan.FromSeconds(1)));
        }

        [Test]
        public void CreateShouldInitializeComments()
        {
            var entity = new WorkItem();

            Assert.That(entity.Comments, Is.Not.Null);
            Assert.That(entity.Comments, Is.Empty);
        }

        [Test]
        public void AddCommentShouldAddNewCommentToList()
        {
            var commentText = "comment_text";
            var userName = "user_name";

            var entity = new WorkItem();
            entity.AddComment(commentText, userName);

            Assert.That(entity.Comments.Count(), Is.EqualTo(1));
            Assert.That(entity.Comments.First().WorkItem, Is.EqualTo(entity));
            Assert.That(entity.Comments.First().Text, Is.EqualTo(commentText));
            Assert.That(entity.Comments.First().User, Is.EqualTo(userName));
        }

    }
}
