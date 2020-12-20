using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler
{
    /// <summary>
    /// Custom Exception that is thrown when the block of statement is invalid or not found.
    /// </summary>
    class InvalidBlockEnd : Exception
    {
        /// <summary>
        /// Error message.
        /// </summary>
        /// <param name="message">Error message.</param>
        public InvalidBlockEnd(string message) : base(message) { }
    }
}
