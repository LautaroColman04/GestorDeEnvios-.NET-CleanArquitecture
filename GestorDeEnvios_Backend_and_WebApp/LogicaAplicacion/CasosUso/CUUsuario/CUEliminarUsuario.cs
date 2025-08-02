using DTOs.DTOs.DTOsUsuario;
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
    public class CUEliminarUsuario : ICUEliminarUsuario
    {
        private IRepositorioUsuario _repoUsuario;
        private IRepositorioAuditoria _repoAuditoria;
        public CUEliminarUsuario(IRepositorioUsuario repoUsuario, IRepositorioAuditoria repoAuditoria)
        {
            _repoUsuario = repoUsuario;
            _repoAuditoria = repoAuditoria;
        }
        public void EliminarUsuario(DTOUsuario dto)
        {
            try
            {
                Usuario usuario = _repoUsuario.FindById((int)dto.Id);
                int r = _repoUsuario.Remove(usuario.Id);

                Auditoria aud = new Auditoria(dto.LogueadoId, "BORRAR", usuario.GetType().Name, r.ToString(), "Eliminado correctamente" + JsonSerializer.Serialize(usuario));
                _repoAuditoria.Auditar(aud);
            }
            catch (Exception ex)
            {
                Auditoria aud = new Auditoria(dto.LogueadoId, "BORRAR", "Usuario", dto.Id.ToString(), ex.Message);
                _repoAuditoria.Auditar(aud);
                throw new Exception("Error al eliminar el usuario: " + ex.Message);
            }
        }
    }
}
