using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using WorkItems.Core.Domain;
using FluentNHibernate.Automapping;

namespace WorkItems.Core.DataAccess.Mappings
{
    public abstract class EntityMap<T> : ClassMap<T> where T : Entity
    {
        public EntityMap()
        {
            Id(x => x.Id);
            Version(x => x.Version);
        }
    }
}
