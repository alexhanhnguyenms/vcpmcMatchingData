using System;

namespace Vcpmc.Mis.Utilities.Exceptions
{
    public class VcpmcException : Exception
    {
        public VcpmcException()
        {
        }

        public VcpmcException(string message)
            : base(message)
        {
        }

        public VcpmcException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
