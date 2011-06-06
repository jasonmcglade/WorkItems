using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using NHibernate;
using FluentNHibernate.Cfg.Db;
using System.Reflection;
using Ninject.Activation;

namespace WorkItems.Core.DataAccess
{
    public class SQLiteSessionFactoryProvider : Provider<ISessionFactory>
    {
        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                    SQLiteConfiguration.Standard
                        .ConnectionString(c => c.FromConnectionStringWithKey("WorkItemsDatabase"))
                        .Raw("connection.release_mode", "on_close")
                )
                .Mappings(m =>
                    m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildSessionFactory();
        }

        protected override ISessionFactory CreateInstance(IContext context)
        {
            return CreateSessionFactory();
        }
    }
}
