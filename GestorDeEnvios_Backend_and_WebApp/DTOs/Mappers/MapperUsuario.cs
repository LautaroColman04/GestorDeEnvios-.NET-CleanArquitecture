using DTOs.DTOs.DTOsUsuario;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enumerados.UsuarioEnums;
using LogicaNegocio.VO.Usuario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Mappers
{
    public class MapperUsuario
    {
        public static Usuario ToUsuario(DTOAltaUsuario dto)
        {

            var r = Roles.Administrador;

            if (dto.Rol.Equals(1))
            {
                r = Roles.Funcionario;
            }else
             if (dto.Rol.Equals(2))
            {
                r = Roles.Cliente;
            } 

            string passHashed = Utilidades.Crypto.HashPasswordConBcrypt(dto.Password, 12);


            Usuario ret = new Usuario(new NombreCompleto(dto.Nombre, dto.Apellido), dto.Email, passHashed, r);

            return ret;

        }

        public static DTOUsuario ToDTOUsuario(Usuario u)
        {
            DTOUsuario dto = new DTOUsuario();
            dto.Id = u.Id;
            dto.Nombre = u.NombreCompleto.Nombre;
            dto.Apellido = u.NombreCompleto.Apellido;
            dto.Email = u.Email;
            dto.Rol = u.Rol.ToString();

            return dto;
        }
        public static List<DTOUsuario> ToListDTOUsuario(List<Usuario> usuarios)
        {
            List<DTOUsuario> dtos = new List<DTOUsuario>();
            foreach (Usuario u in usuarios)
            {
                dtos.Add(ToDTOUsuario(u));
            }
            return dtos;
        }

        public static Usuario ToUsuarioSinPassword(DTOUsuario dto, Usuario usuario)
        {

            var r = Roles.Administrador;

            if (dto.RolInt.Equals(1))
            {
                r = Roles.Funcionario;
            }
            else
             if (dto.RolInt.Equals(2))
            {
                r = Roles.Cliente;
            }

            Usuario ret = new Usuario((int)dto.Id, new NombreCompleto(dto.Nombre, dto.Apellido), dto.Email, usuario.Password, r);

            return ret;

        }

        public static Usuario ToUsuarioConNuevaPassword(DTOCambiarPassword dto, Usuario usuario)
        {
            if (!BCrypt.Net.BCrypt.Verify(dto.PasswordActual, usuario.Password))
            {
                throw new Exception("La contraseña actual es incorrecta");
            }
            if (dto.PasswordActual == dto.PasswordNueva)
            {
                throw new Exception("La contraseña debe ser distinta a la actual");
            }
            string passHashedNueva = Utilidades.Crypto.HashPasswordConBcrypt(dto.PasswordNueva, 12);
            Usuario ret = new Usuario(usuario.Id, usuario.NombreCompleto, usuario.Email, passHashedNueva, usuario.Rol);
            return ret;
        }

    }
}
