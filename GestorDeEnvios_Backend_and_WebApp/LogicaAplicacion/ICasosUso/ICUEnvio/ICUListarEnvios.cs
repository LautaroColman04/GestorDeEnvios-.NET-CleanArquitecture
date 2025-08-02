using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.ICasosUso.ICUEnvio
{
    public interface ICUListarEnvios
    {
        List<DTOs.DTOs.DTOsEnvio.DTOEnvio> ListarEnvios();
    }
}
