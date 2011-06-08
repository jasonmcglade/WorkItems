using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace WorkItems.Core.Tests
{
    public class IntegrationCategory : CategoryAttribute
    {
        public IntegrationCategory()
            : base("Integration")
        {

        }
    }
}
