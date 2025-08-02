using DTOs.DTOs.DTOsUsuario;
using DTOs.Mappers;
using LogicaAplicacion.ICasosUso.ICUUsuario;
using LogicaNegocio.CustomExceptions.UsuarioExceptions;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUCambiarPassword : ICUCambiarPassword
    {
        private IRepositorioUsuario _repoUsuario;

        public CUCambiarPassword(IRepositorioUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }

        public void Ejecutar(DTOCambiarPassword dto, string email)
        {
            if (string.IsNullOrEmpty(dto.PasswordActual) || string.IsNullOrEmpty(dto.PasswordNueva))
            {
                throw new ArgumentNullException("La contraseña actual no puede ser nula o vacía");
            }
            if (dto.PasswordNueva.Length < 8 || dto.PasswordNueva.Length > 32)
            {
                throw new PasswordNoValidaException("La contraseña debe tener al entre 8 y 32 caracteres");
            }
            try
            {
                Usuario usuarioBuscado = _repoUsuario.FindByEmail(email);
                if (usuarioBuscado == null)
                {
                    throw new Exception("Usuario no encontrado");
                }
                Usuario usuario = MapperUsuario.ToUsuarioConNuevaPassword(dto, usuarioBuscado);
                _repoUsuario.Detach(usuarioBuscado);
                _repoUsuario.Update(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar la contraseña: " + ex.Message);
            }
        }
    }
}
