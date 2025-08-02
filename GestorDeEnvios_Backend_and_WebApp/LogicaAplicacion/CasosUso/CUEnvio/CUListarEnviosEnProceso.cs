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
    public class CUListarEnviosEnProceso : ICUListarEnviosEnProceso
    {
        private IRepositorioEnvio _repoEnvio;

        public CUListarEnviosEnProceso(IRepositorioEnvio repoEnvio)
        {
            _repoEnvio = repoEnvio;
        }

        public List<DTOEnvio> ListarEnviosEnProceso()
        {
            try
            {
                List<Envio> envios = _repoEnvio.FindAllEnProceso();

                List<DTOEnvio> listDtoParaRetornar = MapperEnvio.ToListDTOEnvio(envios);
                return listDtoParaRetornar;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar envios en proceso");
            }
        }
    }
}
