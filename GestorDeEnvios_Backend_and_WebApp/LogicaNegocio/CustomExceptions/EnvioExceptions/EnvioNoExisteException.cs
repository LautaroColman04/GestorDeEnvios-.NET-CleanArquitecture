using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.CustomExceptions.EnvioExceptions
{
    public class EnvioNoExisteException : Exception
    {
        public EnvioNoExisteException(string msg) : base(msg)
        {
        }
        public EnvioNoExisteException(string msg, Exception innerException) : base(msg, innerException)
        {
        }
    }
}
