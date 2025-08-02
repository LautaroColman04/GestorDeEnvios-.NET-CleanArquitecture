using DTOs.DTOs.DTOsEnvio;
using DTOs.DTOs.DTOsUsuario;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Mappers
{
    public class MapperEnvio
    {
        public static Envio ToEnvio(DTOCreateEnvio dto)
        {

            Envio nuevo;

            if (dto.TipoEnvio.Equals("URGENTE"))
            {
                nuevo = new Urgente(null, null, (float)dto.Peso, dto.DireccionPostal);
            }
            else if (dto.TipoEnvio.Equals("COMUN"))
            {
                nuevo = new Comun(null, null, (float)dto.Peso, null);
            }
            else
            {
                throw new Exception("Tipo de envio no valido");
            }
            return nuevo;
        }

        public static DTOEnvio ToDTOEnvio(Envio envio)
        {
            DTOEnvio dto = new DTOEnvio();

            if (envio is Comun)  
            {
                Comun comun = (Comun)envio;
                dto.Id = comun.Id;
                dto.NumTracking = comun.NumTracking;
                dto.Peso = comun.Peso;
                dto.AgenciaId = comun.Agencia.Id;
                dto.DireccionPostal = comun.Agencia.direccionPostal;
                dto.Estado = comun.Estado.ToString();
                dto.EmpleadoId = comun.Empleado.Id;
                dto.ClienteId = comun.Cliente.Id;
                dto.FechaCreacion = comun.FechaCreacion;
                dto.FechaEntrega = comun.FechaEntrega;
                foreach (SeguimientoEnvio seguimiento in comun.Seguimientos)
                {
                    dto.DtoSeguimientosEnvio.Add(MapperSeguimientoEnvio.ToDTOSeguimientoEnvio(seguimiento));
                }
                dto.TipoEnvio = "COMUN";
            } 

            else if (envio is Urgente)
            {
                Urgente urgente = (Urgente)envio;
                dto.Id = urgente.Id;
                dto.NumTracking = urgente.NumTracking;
                dto.Peso = urgente.Peso;
                dto.DireccionPostal = urgente.DireccionPostal;
                dto.Estado = urgente.Estado.ToString();
                dto.EmpleadoId = urgente.Empleado.Id;
                dto.ClienteId = urgente.Cliente.Id;
                dto.FechaCreacion = urgente.FechaCreacion;
                dto.FechaEntrega = urgente.FechaEntrega;
                dto.fueEficiente = urgente.FueEficiente;
                foreach (SeguimientoEnvio seguimiento in urgente.Seguimientos)
                {
                    dto.DtoSeguimientosEnvio.Add(MapperSeguimientoEnvio.ToDTOSeguimientoEnvio(seguimiento));
                }
                dto.TipoEnvio = "URGENTE";
            }
            return dto;
        }

        public static List<DTOEnvio> ToListDTOEnvio(List<Envio> envios)
        {
            List<DTOEnvio> dtos = new List<DTOEnvio>();
            foreach (Envio envio in envios)
            {
                dtos.Add(ToDTOEnvio(envio));
            }
            return dtos;
        }

        public static DTOEnvioCliente ToDTOEnvioCliente(Envio envio)
        {
            DTOEnvioCliente dto = new DTOEnvioCliente();

            if (envio is Comun)
            {
                Comun comun = (Comun)envio;
                dto.NumTracking = comun.NumTracking;
                dto.Peso = comun.Peso;
                dto.DireccionPostal = comun.Agencia.direccionPostal;
                dto.Estado = comun.Estado.ToString();
                dto.FechaCreacion = comun.FechaCreacion;
                dto.FechaEntrega = comun.FechaEntrega;
                foreach (SeguimientoEnvio seguimiento in comun.Seguimientos)
                {
                    dto.DtoSeguimientosEnvio.Add(MapperSeguimientoEnvio.ToDTOSeguimientoEnvioCliente(seguimiento));
                }
                dto.TipoEnvio = "COMUN";
            }

            else if (envio is Urgente)
            {
                Urgente urgente = (Urgente)envio;
                dto.NumTracking = urgente.NumTracking;
                dto.Peso = urgente.Peso;
                dto.DireccionPostal = urgente.DireccionPostal;
                dto.Estado = urgente.Estado.ToString();
                dto.FechaCreacion = urgente.FechaCreacion;
                dto.FechaEntrega = urgente.FechaEntrega;
                dto.fueEficiente = urgente.FueEficiente;
                foreach (SeguimientoEnvio seguimiento in urgente.Seguimientos)
                {
                    dto.DtoSeguimientosEnvio.Add(MapperSeguimientoEnvio.ToDTOSeguimientoEnvioCliente(seguimiento));
                }
                dto.TipoEnvio = "URGENTE";
            }
            return dto;
        }

        public static List<DTOEnvioCliente> ToListDTOEnvioCliente(List<Envio> envios)
        {
            List<DTOEnvioCliente> dtos = new List<DTOEnvioCliente>();
            foreach (Envio envio in envios)
            {
                dtos.Add(ToDTOEnvioCliente(envio));
            }
            return dtos;
        }
    }
}
