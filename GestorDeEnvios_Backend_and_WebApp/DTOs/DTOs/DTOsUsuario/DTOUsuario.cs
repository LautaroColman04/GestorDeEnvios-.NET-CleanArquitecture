using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs.DTOsUsuario
{
    public class DTOUsuario
    {
        public string? Email { get; set; }
        public string? Password { get; set; }

        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Rol { get; set; }
        public int? RolInt { get; set; }
        public int? LogueadoId { get; set; }
    }
}
