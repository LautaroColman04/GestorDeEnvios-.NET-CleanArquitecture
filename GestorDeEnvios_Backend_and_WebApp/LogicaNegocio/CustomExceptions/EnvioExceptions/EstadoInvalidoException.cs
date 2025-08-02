using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.CustomExceptions.EnvioExceptions
{
    public class EstadoInvalidoException : Exception
    {
        public EstadoInvalidoException(string? message) : base(message)
        {
        }
        public EstadoInvalidoException(string? message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
