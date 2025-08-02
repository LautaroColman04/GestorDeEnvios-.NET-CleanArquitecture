using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.VO.Agencia;

namespace LogicaNegocio.Entidades
{
    public class Agencia
    {
        public int Id { get; set; }
        public string direccionPostal { get; set; }
        public Ubicacion Ubicacion { get; set; }

        public Agencia() { }
        public Agencia(int id, string direccionPostal, Ubicacion ubicacion)
        {
            this.Id = id;
            this.direccionPostal = direccionPostal;
            Ubicacion = ubicacion;
            Validar();
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(direccionPostal))
            {
                throw new ArgumentNullException("La dirección postal no puede estar vacía");
            }
            if (Ubicacion == null)
            {
                throw new ArgumentNullException("La ubicación no puede ser nula");
            }
            Ubicacion.Validar();
        }
    }
}
