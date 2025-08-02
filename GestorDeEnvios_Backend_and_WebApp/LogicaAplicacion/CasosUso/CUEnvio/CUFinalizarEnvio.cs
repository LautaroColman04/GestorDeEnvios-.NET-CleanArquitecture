using DTOs.DTOs.DTOsEnvio;
using LogicaAplicacion.ICasosUso.ICUEnvio;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enumerados.EnvioEnums;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUEnvio
{
    public class CUFinalizarEnvio : ICUFinalizarEnvio
    {

        private IRepositorioEnvio _repoEnvio;
        private IRepositorioAuditoria _repoAuditoria;
        public CUFinalizarEnvio(IRepositorioEnvio repoEnvio, IRepositorioAuditoria repoAuditoria)
        {
            _repoEnvio = repoEnvio;
            _repoAuditoria = repoAuditoria;
        }
        public void FinalizarEnvio(DTOEnvio dto)
        {
            try
            {
                Envio e = _repoEnvio.FindById((int)dto.Id);
                if (e == null)
                {
                    throw new Exception("Envio no encontrado");
                }
                e.Finalizar();
                int r = _repoEnvio.Update(e);
                // Registrar auditoria
                Auditoria aud = new Auditoria(dto.LogueadoId, "FINALIZAR", e.GetType().Name, r.ToString(), "Actualizacion correcta" + JsonSerializer.Serialize(e));
                _repoAuditoria.Auditar(aud);
            }
            catch (Exception e)
            {
                Auditoria aud = new Auditoria(dto.LogueadoId, "FINALIZAR", "Envio", null, "ERROR: " + e.Message);
                _repoAuditoria.Auditar(aud);
                throw new Exception("Error al finalizar el envio: " + e.Message);
            }
        }
    }
}
