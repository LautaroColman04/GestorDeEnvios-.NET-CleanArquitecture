using DTOs.DTOs.DTOsUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.ICasosUso.ICUUsuario
{
    public interface ICUCambiarPassword
    {
        void Ejecutar(DTOCambiarPassword dto, string email);
    }
}
