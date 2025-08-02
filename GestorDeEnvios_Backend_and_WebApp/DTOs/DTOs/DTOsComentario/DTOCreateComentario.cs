using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs.DTOsComentario
{
    public class DTOCreateComentario
    {
        public int? UsuarioId { get; set; }
        public string? Texto { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
