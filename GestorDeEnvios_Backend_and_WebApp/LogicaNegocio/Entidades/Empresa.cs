using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Empresa
    {
        public int Id { get; set; }
        public List<Agencia> Agencias;

        public Empresa() { }
        public Empresa(int id, List<Agencia> agencias)
        {
            this.Id = id;
            Agencias = agencias;
            //Validar();
        }
        /*public void Validar()
        {
            if (Agencias == null || Agencias.Count == 0)
            {
                throw new ArgumentNullException("La lista de agencias no puede estar vacía");
            }
            foreach (var agencia in Agencias)
            {
                agencia.Validar();
            }
        }*/
    }
}
