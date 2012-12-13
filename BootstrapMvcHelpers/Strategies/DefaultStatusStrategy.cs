using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BootstrapMvcHelpers.Strategies
{
    internal class DefaultStatusStrategy : IStatusStrategy
    {
        public string GetClassForStatus(string type)
        {
            return type;
        }
    }
}
