using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BootstrapMvcHelpers
{
    internal interface IStatusStrategy
    {
        string GetClassForStatus(string type);
    }
}
