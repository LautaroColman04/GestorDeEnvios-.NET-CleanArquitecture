using DTOs.DTOs.DTOsEnvio;
using DTOs.Mappers;
using LogicaAplicacion.ICasosUso.ICUEnvio;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUEnvio
{
    public class CUObtenerEnvio : ICUObtenerEnvio
    {
        private IRepositorioEnvio _repoEnvio;

        public CUObtenerEnvio(IRepositorioEnvio repoEnvio)
        {
            _repoEnvio = repoEnvio;
        }
        public DTOEnvio ObtenerEnvio(int id)
        {
            if (_repoEnvio.FindById(id) == null)
            {
                throw new ArgumentException("El envio no existe");
            }
            try
            {
                Envio e = _repoEnvio.FindById(id);
                DTOEnvio dto = MapperEnvio.ToDTOEnvio(e);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el envio: " + ex.Message);
            }
        }

    }
}
