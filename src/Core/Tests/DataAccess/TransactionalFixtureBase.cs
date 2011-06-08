using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate;
using WorkItems.Core.DataAccess;

namespace Core.Tests.DataAccess
{
    public class TransactionalFixtureBase
    {
        private ISessionFactory _sessionFactory;
        private ISession _session;
        private ITransaction _transaction;

        public ISession Session { get { return _session; } }

        [SetUp]
        public virtual void SetUp()
        {
            _sessionFactory = SQLiteSessionFactoryProvider.CreateSessionFactory();
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

        public T Save<T>(T entity)
        {
            _session.SaveOrUpdate(entity);

            _session.Flush();
            _session.Clear();

            _session.Evict(entity);

            return entity;
        }

        public T Get<T>(int id)
        {
            return _session.Get<T>(id);
        }
    }
}
