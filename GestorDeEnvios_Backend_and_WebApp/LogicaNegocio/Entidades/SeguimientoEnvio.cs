 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class SeguimientoEnvio
    {
        public int Id { get; set; }
        public Comentario Comentario { get; set; }

        public SeguimientoEnvio() { }
        public SeguimientoEnvio(Comentario comentario)
        {
            Comentario = comentario;
            Validar();
        }
        public void Validar()
        {
            if (Comentario == null)
            {
                throw new ArgumentNullException("El comentario no puede ser nulo");
            }
            Comentario.Validar();
        }
    }
}
