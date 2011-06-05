﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using NHibernate;
using FluentNHibernate.Cfg.Db;
using System.Reflection;

namespace WorkItems.Core.DataAccess
{
    public static class SQLiteSessionFactory
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
    }
}
