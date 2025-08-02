using DTOs.DTOs.DTOsUsuario;
using DTOs.Mappers;
using LogicaAplicacion.ICasosUso.ICUUsuario;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUActualizarUsuario : ICUActualizarUsuario
    {
        private IRepositorioUsuario _repoUsuario;
        private IRepositorioAuditoria _repoAuditoria;

        public CUActualizarUsuario(IRepositorioUsuario repoUsuario, IRepositorioAuditoria repoAuditoria)
        {
            _repoUsuario = repoUsuario;
            _repoAuditoria = repoAuditoria;
        }
        public void ActualizarUsuario(DTOUsuario dto)
        {
            if (string.IsNullOrEmpty(dto.Email))
            {
                throw new ArgumentNullException("El email no puede ser nulo o vacío");
            }
            if (!dto.Email.Contains("@"))
            {
                throw new FormatException("El email no tiene un formato válido");
            }
            if (string.IsNullOrEmpty(dto.Nombre))
            {
                throw new ArgumentNullException("El nombre no puede ser nulo o vacío");
            }
            if (string.IsNullOrEmpty(dto.Apellido))
            {
                throw new ArgumentNullException("El nombre no puede ser nulo o vacío");
            }
            try
            {
                Usuario buscado = _repoUsuario.FindById((int)dto.Id);

                Usuario usuario = MapperUsuario.ToUsuarioSinPassword(dto, buscado);
                _repoUsuario.Detach(buscado);
                int r = _repoUsuario.Update(usuario);

                Auditoria aud = new Auditoria(dto.LogueadoId, "ACTUALIZAR", usuario.GetType().Name, r.ToString(), "Modificacion correcta" + JsonSerializer.Serialize(usuario));
                _repoAuditoria.Auditar(aud);
            }
            catch (Exception ex)
            {
                Auditoria aud = new Auditoria(dto.LogueadoId, "ACTUALIZAR", "Usuario", dto.Id.ToString(), "Error en la modificacion" + ex.Message);
                _repoAuditoria.Auditar(aud);
                throw new Exception("Error al actualizar el usuario: " + ex.Message);

            }
        }
    }
}
