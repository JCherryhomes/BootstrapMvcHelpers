using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BootstrapMvcHelpers
{
    internal class InfoStatusStrategy : IStatusStrategy
    {
        public string GetClassForStatus(string type)
        {
            return string.Format("{0} {0}-info", type);
        }
    }
}
