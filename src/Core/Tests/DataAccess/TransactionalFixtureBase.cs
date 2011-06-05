using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate;
using WorkItems.Core.DataAccess;

namespace Core.Tests.DataAccess
{
    [TestFixture]
    public class TransactionalFixtureBase
    {
        private ISessionFactory _sessionFactory;
        private ISession _session;
        private ITransaction _transaction;

        public ISession Session { get { return _session; } }

        [SetUp]
        public virtual void SetUp()
        {
            _sessionFactory = SQLiteSessionFactory.CreateSessionFactory();
            _session = _sessionFactory.OpenSession();
            _transaction = _session.BeginTransaction();
        }

        [TearDown]
        public virtual void TearDown()
        {
            if (_transaction != null)
            {
                if (_transaction.IsActive)
                {
                    _transaction.Rollback();
                }
                _transaction.Dispose();
            }

            if (_session != null)
            {
                _session.Close();
                _session.Dispose();
            }

            if (_sessionFactory != null)
            {
                _sessionFactory.Dispose();
            }
        }

        public void Save<T>(T entity)
        {
            _session.Save(entity);

            _session.Flush();
            _session.Clear();
        }
    }
}
