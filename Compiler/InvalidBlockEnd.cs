using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler
{
    class InvalidBlockEnd : Exception
    {
        public InvalidBlockEnd(string message) : base(message) { }
    }
}
