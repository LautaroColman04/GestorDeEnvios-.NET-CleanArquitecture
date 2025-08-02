using DTOs.DTOs.DTOsUsuario;
using DTOs.Mappers;
using LogicaAplicacion.ICasosUso.ICUUsuario;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUObtenerUsuario : ICUObtenerUsuario
    {
        private IRepositorioUsuario _repoUsuario;
        public CUObtenerUsuario(IRepositorioUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }

        public DTOUsuario ObtenerUsuario(int id)
        {
            if (_repoUsuario.FindById(id) == null)
            {
                throw new ArgumentException("El id no existe");
            }
            try
            {
                Usuario usuario = _repoUsuario.FindById(id);
                DTOUsuario dto = MapperUsuario.ToDTOUsuario(usuario);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario: " + ex.Message);
            }
        }
    }
}
