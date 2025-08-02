using DTOs.DTOs.DTOsComentario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs.DTOsSeguimiento
{
    public class DTOCreateSeguimientoEnvio
    {
        public DTOCreateComentario? dtoComentario { get; set; }
        public int? EnvioId { get; set; }
    }
}
