using DTOs.DTOs.DTOsEnvio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.ICasosUso.ICUEnvio
{
    public interface ICUListarEnviosDelUsuarioPorComentario
    {
        List<DTOEnvioCliente> Ejecutar(string email, string comentario);
    }
}
