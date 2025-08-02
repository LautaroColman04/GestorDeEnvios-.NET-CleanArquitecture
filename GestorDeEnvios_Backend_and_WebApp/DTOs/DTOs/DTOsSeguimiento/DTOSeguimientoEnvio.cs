using DTOs.DTOs.DTOsComentario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs.DTOsSeguimiento
{
    public class DTOSeguimientoEnvio
    {
        public int? Id { get; set; }
        public DTOComentario? dtoComentario { get; set; }
    }
}
