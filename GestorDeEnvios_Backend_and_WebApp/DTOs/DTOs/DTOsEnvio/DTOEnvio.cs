using DTOs.DTOs.DTOsSeguimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs.DTOsEnvio
{
    public class DTOEnvio
    {
        public int? Id { get; set; }
        public string? TipoEnvio { get; set; }
        public int? NumTracking { get; set; }
        public int? EmpleadoId { get; set; }
        public int? ClienteId { get; set; }
        public int? AgenciaId { get; set; }
        public string? DireccionPostal { get; set; }
        public float? Peso { get; set; }
        public string? Estado { get; set; }
        public List<DTOSeguimientoEnvio>? DtoSeguimientosEnvio { get; set; } = new List<DTOSeguimientoEnvio>();
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public bool? fueEficiente { get; set; }

        public int? LogueadoId { get; set; }
    }
}
