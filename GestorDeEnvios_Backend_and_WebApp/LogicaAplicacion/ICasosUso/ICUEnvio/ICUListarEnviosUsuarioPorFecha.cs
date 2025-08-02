using DTOs.DTOs.DTOsEnvio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.ICasosUso.ICUEnvio
{
    public interface ICUListarEnviosUsuarioPorFecha
    {
        public List<DTOEnvioCliente> Ejecutar(string email, DateTime fecha1, DateTime fecha2, string estado);
    }
}
