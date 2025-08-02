using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Enumerados.EnvioEnums;

namespace LogicaNegocio.Entidades
{
    public class Comun : Envio
    {
        public Agencia Agencia { get; set; }
        public Comun():base(){ }
        public Comun(Usuario empleado, Usuario cliente, float peso, Agencia agencia) : base(empleado, cliente, peso)
        {
            Agencia = agencia;
            Validar();
        }
        public override void Validar()
        {
            base.Validar();
        }
    }
}
