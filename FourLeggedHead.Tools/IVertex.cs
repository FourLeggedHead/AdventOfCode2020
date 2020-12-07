using System;
using System.Collections.Generic;
using System.Text;

namespace FourLeggedHead.Tools
{
    public interface IVertex
    {
        IVertex Parent { get; set; }
    }
}
