using DTOs.DTOs.DTOsSeguimiento;
using DTOs.Mappers;
using LogicaAplicacion.ICasosUso.ICUEnvio;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUEnvio
{
    public class CUCreateSeguimientoEnvio : ICUCreateSeguimientoEnvio
    {
        private IRepositorioUsuario _repoUsuario;
        private IRepositorioSeguimientoEnvio _repoSeguimientoEnvio;
        private IRepositorioEnvio _repoEnvio;
        private IRepositorioAuditoria _repoAuditoria;
        private IRepositorioComentario _repoComentario;

        public CUCreateSeguimientoEnvio(IRepositorioUsuario repoUsuario, IRepositorioSeguimientoEnvio repoSeguimientoEnvio, IRepositorioEnvio repoEnvio, IRepositorioAuditoria repoAuditoria, IRepositorioComentario repoComentario)
        {
            _repoUsuario = repoUsuario;
            _repoEnvio = repoEnvio;
            _repoSeguimientoEnvio = repoSeguimientoEnvio;
            _repoAuditoria = repoAuditoria;
            _repoComentario = repoComentario;
        }

        public void CreateSeguimientoEnvio(DTOCreateSeguimientoEnvio dto)
        {
            try
            {
                Usuario empleado = _repoUsuario.FindById(dto.dtoComentario.UsuarioId.Value);
                Envio envio = _repoEnvio.FindById(dto.EnvioId.Value);

                SeguimientoEnvio nuevo = MapperSeguimientoEnvio.ToSeguimientoEnvio(dto);
                nuevo.Comentario.Empleado = empleado;
                envio.Seguimientos.Add(nuevo);
                int idEntidad = _repoEnvio.Update(envio);

                //int idEntidad = _repoSeguimientoEnvio.Add(nuevo);
                Auditoria aud = new Auditoria(dto.dtoComentario.UsuarioId, "ALTA", nuevo.GetType().Name, idEntidad.ToString(), "Alta correcta" + JsonSerializer.Serialize(nuevo));
                _repoAuditoria.Auditar(aud);
            }
            catch (Exception e)
            {
                Auditoria aud = new Auditoria(dto.dtoComentario.UsuarioId, "ALTA", "SeguimientoEnvio", null, "ERROR: " + e.Message);
                _repoAuditoria.Auditar(aud);
                throw new Exception("Error al crear el seguimiento: " + e.Message);
            }
        }
    }
}
