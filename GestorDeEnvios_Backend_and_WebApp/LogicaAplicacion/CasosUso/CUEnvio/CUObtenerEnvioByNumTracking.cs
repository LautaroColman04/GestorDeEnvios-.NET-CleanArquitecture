using DTOs.DTOs.DTOsEnvio;
using DTOs.Mappers;
using LogicaAplicacion.ICasosUso.ICUEnvio;
using LogicaNegocio.CustomExceptions.EnvioExceptions;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUEnvio
{
    public class CUObtenerEnvioByNumTracking : ICUObtenerEnvioByNumTracking
    {
        private IRepositorioEnvio _repoEnvio;
        public CUObtenerEnvioByNumTracking(IRepositorioEnvio repoEnvio)
        {
            _repoEnvio = repoEnvio;
        }
        public DTOEnvioCliente ObtenerEnvio(int numTracking)
        {
            if (numTracking == 0)
            {
                throw new Exception("El numero de tracking no puede ser nulo");
            }
            if (numTracking < 0)
            {
                throw new Exception("El numero de tracking no puede ser negativo");
            }
            if (numTracking > 999999 || numTracking < 100000)
            {
                throw new Exception("El numero de tracking tiene que ser de 6 digitos");
            }
            if (_repoEnvio.FindByNumTracking(numTracking) == null)
            {
                throw new EnvioNoExisteException("El envio no existe");
            }
            try
            {
                Envio e = _repoEnvio.FindByNumTracking(numTracking);
                DTOEnvioCliente dto = MapperEnvio.ToDTOEnvioCliente(e);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el envio por numero de tracking", ex);
            }
        }
    }
}
