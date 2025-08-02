using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs.DTOsEnvio
{
    public class DTOCreateEnvio
    {
        [Required]
        public string TipoEnvio { get; set; }
        [Required]
        [EmailAddress]
        public string ClienteEmail { get; set; }
        public int? AgenciaId { get; set; }
        public string? DireccionPostal { get; set; }
        [Required]
        public float Peso { get; set; }

        public int? LogueadoId { get; set; }
    }
}
