using DTOs.DTOs.DTOsSeguimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs.DTOsEnvio
{
    public class DTOEnvioCliente
    {
        public string? TipoEnvio { get; set; }
        public int? NumTracking { get; set; }
        public string? DireccionPostal { get; set; }
        public float? Peso { get; set; }
        public string? Estado { get; set; }
        public List<DTOSeguimientoEnvioCliente>? DtoSeguimientosEnvio { get; set; } = new List<DTOSeguimientoEnvioCliente>();
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public bool? fueEficiente { get; set; }
    }
}
