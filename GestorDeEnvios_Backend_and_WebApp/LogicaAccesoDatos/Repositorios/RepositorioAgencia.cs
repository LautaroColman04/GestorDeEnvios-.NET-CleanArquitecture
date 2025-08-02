using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioAgencia : IRepositorioAgencia
    {
        private ApplicationDbContext _context;
        public RepositorioAgencia(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Add(Agencia nuevo)
        {
            throw new NotImplementedException();
        }

        public List<Agencia> FindAll()
        {
            throw new NotImplementedException();
        }

        public Agencia FindById(int id)
        {
            return _context.Agencias.Find(id);
        }

        public int Remove(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Agencia obj)
        {
            throw new NotImplementedException();
        }
    }
}
