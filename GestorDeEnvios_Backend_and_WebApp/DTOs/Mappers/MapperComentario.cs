using DTOs.DTOs.DTOsComentario;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Mappers
{
    public class MapperComentario
    {
        public static DTOComentario ToDTOComentario(Comentario comentario)
        {
            DTOComentario dto = new DTOComentario();

            dto.Id = comentario.Id;
            dto.UsuarioId = comentario.Empleado.Id;
            dto.UsuarioNombre = comentario.Empleado.NombreCompleto.Nombre;
            dto.Texto = comentario.Texto;
            dto.Fecha = comentario.Fecha;

            return dto;
        }

        public static DTOComentarioCliente ToDTOComentarioCliente(Comentario comentario)
        {
            DTOComentarioCliente dto = new DTOComentarioCliente();

            dto.UsuarioNombre = comentario.Empleado.NombreCompleto.Nombre;
            dto.Texto = comentario.Texto;
            dto.Fecha = comentario.Fecha;

            return dto;
        }
    }
}
