using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.CustomExceptions.EnvioExceptions
{
    public class ErrorAlListarEnviosException : Exception
    {
        public ErrorAlListarEnviosException(string? message) : base(message)
        {
        }
        public ErrorAlListarEnviosException(string? message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
