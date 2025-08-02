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
using System.Text.Json;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUAltaUsuario : ICUAltaUsuario
    {
        private IRepositorioUsuario _repoUsuario;
        private IRepositorioAuditoria _repoAuditoria;

        public CUAltaUsuario(IRepositorioUsuario repoUsuario, IRepositorioAuditoria repoAuditoria)
        {
            _repoUsuario = repoUsuario;
            _repoAuditoria = repoAuditoria;
        }

        public void AltaUsuario(DTOAltaUsuario dto)
        {
            if (string.IsNullOrEmpty(dto.Email))
            {
                throw new ArgumentNullException("El email no puede ser nulo o vacío");
            }
            if (!dto.Email.Contains("@"))
            {
                throw new FormatException("El email no tiene un formato válido");
            }
            if (string.IsNullOrEmpty(dto.Password))
            {
                throw new ArgumentNullException("La contraseña no puede ser nula o vacía");
            }
            if (dto.Password.Length < 8 || dto.Password.Length > 32)
            {
                throw new ArgumentException("La contraseña debe tener al entre 8 y 32 caracteres");
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
                Usuario buscado = _repoUsuario.FindByEmail(dto.Email);
                if (buscado != null)
                {
                    throw new EmailYaExisteException("El email ya existe");
                }


                Usuario nuevo = MapperUsuario.ToUsuario(dto);
                int idEntidad = _repoUsuario.Add(nuevo);

                Auditoria aud = new Auditoria(dto.LogueadoId, "ALTA", nuevo.GetType().Name, idEntidad.ToString(), "Alta correcta" + JsonSerializer.Serialize(nuevo));

                _repoAuditoria.Auditar(aud);
            }
            catch (Exception e)
            {
                Auditoria aud = new Auditoria(dto.LogueadoId, "ALTA", "Usuario", null, "ERROR: " + e.Message);

                _repoAuditoria.Auditar(aud);

                throw new Exception("Error al crear el usuario: " + e.Message);
            }

        }
    }
}
