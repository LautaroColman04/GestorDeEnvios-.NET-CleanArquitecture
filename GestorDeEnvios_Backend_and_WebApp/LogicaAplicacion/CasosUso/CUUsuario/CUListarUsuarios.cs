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
    public class CUListarUsuarios : ICUListarUsuarios
    {
        private IRepositorioUsuario _repoUsuario;

        public CUListarUsuarios(IRepositorioUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }

        public List<DTOUsuario> ListarUsuarios()
        {
            try
            {
                List<Usuario> usuarios = _repoUsuario.FindAll();

                List<DTOUsuario> listDtoParaRetornar = MapperUsuario.ToListDTOUsuario(usuarios);
                return listDtoParaRetornar;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar usuarios");
            }
        }
    }
}
