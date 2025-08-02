using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Enumerados.UsuarioEnums;
using LogicaNegocio.VO.Agencia;
using LogicaNegocio.VO.Usuario;

namespace LogicaNegocio.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public NombreCompleto NombreCompleto { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Roles Rol { get; set; }

        public Usuario()
        {   
        }
        public Usuario(NombreCompleto nombreCompleto, string email, string password, Roles rol)
        {
            NombreCompleto = nombreCompleto;
            Email = email;
            Password = password;
            Rol = rol;
            Validar();
        }
        public Usuario(int id,NombreCompleto nombreCompleto, string email, string password, Roles rol)
        {
            Id = id;
            NombreCompleto = nombreCompleto;
            Email = email;
            Password = password;
            Rol = rol;
            Validar();
        }

        public void Validar()
        {
            if (NombreCompleto == null)
            {
                throw new ArgumentNullException("El nombre completo no puede ser nulo");
            }
            if (string.IsNullOrEmpty(Email))
            {
                throw new ArgumentNullException("El email no puede ser nulo o vacío");
            }
            if (!Email.Contains("@"))
            {
                throw new FormatException("El email no tiene un formato válido");
            }
            if (!Enum.IsDefined(typeof(Roles), Rol))
            {
                throw new ArgumentOutOfRangeException("El rol no es válido");
            }

        }
    }
}
