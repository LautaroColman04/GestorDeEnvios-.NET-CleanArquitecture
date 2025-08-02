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
    public class CUListarEnvios : ICUListarEnvios
    {
        private IRepositorioEnvio _repoEnvio;

        public CUListarEnvios(IRepositorioEnvio repoEnvio)
        {
            _repoEnvio = repoEnvio;
        }

        public List<DTOEnvio> ListarEnvios()
        {
            try
            {
                List<Envio> envios = _repoEnvio.FindAll();

                List<DTOEnvio> listDtoParaRetornar = MapperEnvio.ToListDTOEnvio(envios);
                return listDtoParaRetornar;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar envios");
            }
        }
    }
}
