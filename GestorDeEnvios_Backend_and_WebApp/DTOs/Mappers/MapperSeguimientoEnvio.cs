using DTOs.DTOs.DTOsSeguimiento;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Mappers
{
    public class MapperSeguimientoEnvio
    {
        public static SeguimientoEnvio ToSeguimientoEnvio(DTOCreateSeguimientoEnvio dto)
        {
            Comentario comentario = new Comentario(null, dto.dtoComentario.Texto);
            SeguimientoEnvio nuevo = new SeguimientoEnvio(comentario);
            return nuevo;
        }

        public static DTOSeguimientoEnvio ToDTOSeguimientoEnvio(SeguimientoEnvio seguimientoEnvio)
        {
            DTOSeguimientoEnvio dto = new DTOSeguimientoEnvio();
            dto.Id = seguimientoEnvio.Id;
            dto.dtoComentario = MapperComentario.ToDTOComentario(seguimientoEnvio.Comentario);
            return dto;
        }

        public static DTOSeguimientoEnvioCliente ToDTOSeguimientoEnvioCliente(SeguimientoEnvio seguimientoEnvio)
        {
            DTOSeguimientoEnvioCliente dto = new DTOSeguimientoEnvioCliente();

            dto.dtoComentario = MapperComentario.ToDTOComentarioCliente(seguimientoEnvio.Comentario);
            return dto;
        }
    }
}
