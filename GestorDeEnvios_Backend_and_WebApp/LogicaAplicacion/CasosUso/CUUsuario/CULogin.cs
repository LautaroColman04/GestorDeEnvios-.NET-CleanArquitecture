using DTOs.DTOs.DTOsUsuario;
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
    public class CULogin : ICULogin
    {
        private IRepositorioUsuario _repoUsuario;

        public CULogin(IRepositorioUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }

        public DTOUsuario VerificarDatosParaLogin(DTOUsuario dto)
        {
            try
            {
                Usuario u = _repoUsuario.FindByEmail(dto.Email);
                if (u is null)
                {
                    throw new Exception("Datos incorrectos");
                }

                bool coincideElPassword = Utilidades.Crypto.VerifyPasswordConBcrypt(dto.Password, u.Password);

                if (coincideElPassword)
                {
                    DTOUsuario ret = new DTOUsuario();
                    ret.Id = u.Id;
                    ret.Rol = u.Rol.ToString();
                    ret.Email = u.Email;
                    ret.Nombre = u.NombreCompleto.Nombre;
                    ret.Apellido = u.NombreCompleto.Apellido;
                    return ret;
                }
                else
                {

                    throw new Exception("Error de credenciales");
                }
            }
            catch (Exception e)
            {

                throw e;
            }


        }
    }
}
