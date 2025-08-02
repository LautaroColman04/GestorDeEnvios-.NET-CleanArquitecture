using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Comentario
    {
        public int Id { get; set; }
        public Usuario Empleado { get; set; }
        public string Texto { get; set; }
        public DateTime Fecha { get; set; }

        public Comentario() { }
        public Comentario(Usuario funcionario, string texto)
        {
            Empleado = funcionario;
            Texto = texto;
            Fecha = DateTime.Now;
            Validar();
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Texto))
            {
                throw new ArgumentNullException("El texto no puede estar vacío");
            }
        }
    }
}
