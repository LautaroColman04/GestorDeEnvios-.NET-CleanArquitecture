using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioAuditoria : IRepositorioAuditoria
    {
        private ApplicationDbContext _context;
        public RepositorioAuditoria(ApplicationDbContext context)
        {
            _context = context;
        }
        public int Add(Auditoria nuevo)
        {
            throw new NotImplementedException();
        }
        public List<Auditoria> FindAll()
        {
            throw new NotImplementedException();
        }
        public Auditoria FindById(int id)
        {
            throw new NotImplementedException();
        }
        public int Remove(int id)
        {
            throw new NotImplementedException();
        }
        public int Update(Auditoria obj)
        {
            throw new NotImplementedException();
        }
        public void Auditar(Auditoria nueva)
        {
            _context.Auditorias.Add(nueva);
            _context.SaveChanges();
        }
    }
}
