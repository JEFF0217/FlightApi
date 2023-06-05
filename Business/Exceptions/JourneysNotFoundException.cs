using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class JourneysNotFoundException : Exception
    {
        public JourneysNotFoundException()
        {
        }

        public JourneysNotFoundException(string message) : base(message)
        {
        }

        public JourneysNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
