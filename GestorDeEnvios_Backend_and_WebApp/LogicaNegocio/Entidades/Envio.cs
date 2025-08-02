using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Enumerados.EnvioEnums;

namespace LogicaNegocio.Entidades
{
    public abstract class Envio
    {
        public int Id { get; set; }
        public int NumTracking { get; set; }

        public int EmpleadoId { get; set; }
        public Usuario Empleado { get; set; }
        public int ClienteId { get; set; }
        public Usuario Cliente { get; set; }
        public float Peso { get; set; }
        public EstadoEnvio Estado { get; set; }
        public List<SeguimientoEnvio> Seguimientos { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaEntrega { get; set; }

        public Envio() { }
        public Envio(Usuario empleado, Usuario cliente, float peso)
        {
            NumTracking = new Random().Next(100000, 999999);
            Empleado = empleado;
            Cliente = cliente;
            Peso = peso;
            Estado = EstadoEnvio.EnProceso;
            FechaCreacion = DateTime.Now;
            Seguimientos = new List<SeguimientoEnvio>();
            Validar();
        }

        public virtual void Finalizar()
        {
            Estado = EstadoEnvio.Finalizado;
            FechaEntrega = DateTime.Now;
        }

        public virtual void Validar()
        {
            if (Peso <= 0)
            {
                throw new ArgumentOutOfRangeException("El peso debe ser mayor a cero");
            }
        }
    }
}
