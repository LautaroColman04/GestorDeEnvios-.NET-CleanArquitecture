using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Enumerados.EnvioEnums;

namespace LogicaNegocio.Entidades
{
    public class Urgente : Envio
    {
        public string DireccionPostal { get; set; }
        public bool FueEficiente { get; set; }

        public Urgente() : base() { }
        public Urgente(Usuario empleado, Usuario cliente, float peso, string direccionPostal) : base(empleado, cliente, peso)
        {
            DireccionPostal = direccionPostal;
            FueEficiente = false;
            Validar();
        }

        public override void Finalizar()
        {
            base.Finalizar();
            if (FechaEntrega != null)
            {
                TimeSpan tiempoTranscurrido = (DateTime)FechaEntrega - FechaCreacion;
                if (tiempoTranscurrido.TotalHours <= 24)
                {
                    FueEficiente = true;
                }
            }
        }
        public override void Validar()
        {
            base.Validar();
        }
    }
}
