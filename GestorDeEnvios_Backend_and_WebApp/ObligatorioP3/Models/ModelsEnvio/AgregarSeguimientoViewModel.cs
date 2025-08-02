using DTOs.DTOs.DTOsEnvio;
using DTOs.DTOs.DTOsSeguimiento;

namespace ObligatorioP3.Models.ModelsEnvio
{
    public class AgregarSeguimientoViewModel
    {
        public DTOEnvio? dtoEnvio { get; set; }
        public DTOCreateSeguimientoEnvio? dtoCreateSeguimientoEnvio { get; set; }
    }
}
