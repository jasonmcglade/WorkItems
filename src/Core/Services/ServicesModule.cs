using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Modules;
using WorkItems.Core.Services;
using NHibernate;
using WorkItems.Core.DataAccess;

namespace WorkItems.Web.Modules
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISessionFactory>().ToProvider(new SQLiteSessionFactoryProvider()).InSingletonScope();
            Bind<ISession>().ToMethod(context => context.Kernel.Get<ISessionFactory>().OpenSession()).InRequestScope();

            Bind<IWorkItemService>().To<WorkItemService>();
        }
    }
}