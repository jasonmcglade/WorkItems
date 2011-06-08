using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkItems.Core.Domain
{
    public abstract class Entity
    {
        public virtual int Id { get; set; }
        public virtual int Version { get; set; }

        public virtual bool Equals(Entity other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(other, this))
            {
                return true;
            }

            return other.Version.Equals(Version) && other.Id.Equals(Id);
        }

        public override bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }

            if (other is Entity)
            {
                return Equals((Entity)other);
            }

            return base.Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
