using DTOs.DTOs.DTOsEnvio;
using DTOs.DTOs.DTOsUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.ICasosUso.ICUEnvio
{
    public interface ICUCreateEnvio
    {
        void CreateEnvio(DTOCreateEnvio dto);
    }
}
