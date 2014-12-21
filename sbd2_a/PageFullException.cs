using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbd2_a
{
    class PageFullException : Exception
    {
        public PageFullException() { }
        public PageFullException(string message) : base(message) { }
        public PageFullException(string message, Exception inner) : base(message, inner) { }
    }
    class PageReadFaultyException: Exception
    {
        public PageReadFaultyException() { }
        public PageReadFaultyException(string message) : base(message) { }
        public PageReadFaultyException(string message, Exception inner) : base(message, inner) { }
    }
}
