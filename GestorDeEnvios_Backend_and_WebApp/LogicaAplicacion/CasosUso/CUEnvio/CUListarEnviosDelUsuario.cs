using DTOs.DTOs.DTOsEnvio;
using DTOs.DTOs.DTOsUsuario;
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
    public class CUListarEnviosDelUsuario : ICUListarEnviosDelUsuario
    {
        private IRepositorioEnvio _repoEnvio;
        private IRepositorioUsuario _repoUsuario;
        public CUListarEnviosDelUsuario(IRepositorioEnvio repoEnvio, IRepositorioUsuario repoUsuario)
        {
            _repoEnvio = repoEnvio;
            _repoUsuario = repoUsuario;
        }

        public List<DTOEnvioCliente> Ejecutar(string email)
        {
            try
            {
                Usuario usuario = _repoUsuario.FindByEmail(email);
                List<Envio> envios = _repoEnvio.FindAllByUser(usuario.Id);
                List<DTOEnvioCliente> dtos = MapperEnvio.ToListDTOEnvioCliente(envios);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new ErrorAlListarEnviosException("Error al listar los envíos del usuario");
            }
            }
    }
}
