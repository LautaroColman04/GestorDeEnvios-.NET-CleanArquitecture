namespace ClienteHTTP.Models
{
    public class DTOEnvio
    {
        public string? TipoEnvio { get; set; }
        public int? NumTracking { get; set; }
        public string? DireccionPostal { get; set; }
        public float? Peso { get; set; }
        public string? Estado { get; set; }
        public List<DTOSeguimientoEnvio>? DtoSeguimientosEnvio { get; set; } = new List<DTOSeguimientoEnvio>();
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public bool? fueEficiente { get; set; }
    }
}
