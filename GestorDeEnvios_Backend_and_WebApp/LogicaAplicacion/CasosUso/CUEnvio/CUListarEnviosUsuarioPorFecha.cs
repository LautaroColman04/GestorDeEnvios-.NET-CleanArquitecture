using DTOs.DTOs.DTOsEnvio;
using DTOs.Mappers;
using LogicaAplicacion.ICasosUso.ICUEnvio;
using LogicaNegocio.CustomExceptions.EnvioExceptions;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enumerados.EnvioEnums;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUEnvio
{
    public class CUListarEnviosUsuarioPorFecha : ICUListarEnviosUsuarioPorFecha
    {
        private IRepositorioEnvio _repoEnvio;
        private IRepositorioUsuario _repoUsuario;
        public CUListarEnviosUsuarioPorFecha(IRepositorioEnvio repoEnvio, IRepositorioUsuario repoUsuario)
        {
            _repoEnvio = repoEnvio;
            _repoUsuario = repoUsuario;
        }
        public List<DTOEnvioCliente> Ejecutar(string email, DateTime fecha1, DateTime fecha2, string estado)
        {
            if (fecha1 > fecha2)
            {
                throw new Exception("La primer fecha no puede ser mayor que la segunda fecha.");
            }
            if (fecha1 == null || fecha2 == null)
            {
                throw new ArgumentNullException("Las fechas no pueden ser vacias.");
            }
            if (estado != null && estado != "Finalizado" && estado != "EnProceso")
            {
                throw new EstadoInvalidoException("El estado del envío no es válido. Debe ser 'Finalizado' o 'EnProceso'.");
            }
            try
            {
            Usuario usuario = _repoUsuario.FindByEmail(email);
            List<Envio> envios;
                if (estado == null)
                {
                    envios = _repoEnvio.FindAllByUserAndDate(usuario.Id, fecha1, fecha2);
                }
                else
                {
                    EstadoEnvio estadoEnvio = Enum.Parse<EstadoEnvio>(estado);
                    envios = _repoEnvio.FindAllByUserAndDateAndStatus(usuario.Id, fecha1, fecha2, estadoEnvio);
                }

                List<DTOEnvioCliente> dtos = MapperEnvio.ToListDTOEnvioCliente(envios);

                return dtos;
            }
            catch (Exception ex)
            {
                throw new ErrorAlListarEnviosException("Error al listar los envíos del usuario por fecha");
            }
        }
    }
}
