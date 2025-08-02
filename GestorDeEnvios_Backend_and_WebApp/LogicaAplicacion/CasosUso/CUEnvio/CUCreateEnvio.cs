using DTOs.DTOs.DTOsEnvio;
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
    public class CUCreateEnvio : ICUCreateEnvio
    {
        private IRepositorioUsuario _repoUsuario;
        private IRepositorioEnvio _repoEnvio;
        private IRepositorioAgencia _repoAgencia;   
        private IRepositorioAuditoria _repoAuditoria;

        public CUCreateEnvio(IRepositorioUsuario repoUsuario, IRepositorioEnvio repoEnvio, IRepositorioAgencia repoAgencia, IRepositorioAuditoria repoAuditoria)
        {
            _repoUsuario = repoUsuario;
            _repoEnvio = repoEnvio;
            _repoAgencia = repoAgencia;
            _repoAuditoria = repoAuditoria;
        }

        public void CreateEnvio(DTOCreateEnvio dto)
        {
            try
            {
                
                Usuario cliente = _repoUsuario.FindByEmail(dto.ClienteEmail);
                if (cliente == null)
                {
                    throw new Exception("El cliente no existe");
                }

                Usuario empleado = _repoUsuario.FindById(dto.LogueadoId.Value);
                Envio nuevo = MapperEnvio.ToEnvio(dto);

                nuevo.Cliente = cliente;
                nuevo.Empleado = empleado;
                if (nuevo is Comun)
                {
                    Comun comun = (Comun)nuevo;
                    comun.Agencia = _repoAgencia.FindById(dto.AgenciaId.Value);
                    if (comun.Agencia == null)
                    {
                        throw new Exception("La agencia no existe");
                    }
                }

                int idEntidad = _repoEnvio.Add(nuevo);

                Auditoria aud = new Auditoria(dto.LogueadoId, "ALTA", nuevo.GetType().Name, idEntidad.ToString(), "Alta correcta" + JsonSerializer.Serialize(nuevo));
                _repoAuditoria.Auditar(aud);  
            }
            catch (Exception e)
            {
                Auditoria aud = new Auditoria(dto.LogueadoId, "ALTA", "Envio", null, "ERROR: " + e.Message);
                _repoAuditoria.Auditar(aud);
                throw new Exception("Error al crear el envio: " + e.Message);
            }

        }
    }
}
