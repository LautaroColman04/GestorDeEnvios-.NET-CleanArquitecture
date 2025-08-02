using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.CustomExceptions.UsuarioExceptions
{
    public class PasswordNoValidaException : Exception
    {
        public PasswordNoValidaException(string msg) : base(msg)
        {
        }
        public PasswordNoValidaException(string msg, Exception innerException) : base(msg, innerException)
        {
        }
    }
}
