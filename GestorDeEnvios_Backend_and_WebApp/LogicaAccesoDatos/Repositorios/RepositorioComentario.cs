using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioComentario : IRepositorioComentario
    {
        private ApplicationDbContext _context;
        public RepositorioComentario(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Add(Comentario nuevo)
        {
            _context.Comentarios.Add(nuevo);
            _context.SaveChanges();
            return nuevo.Id;
        }

        public List<Comentario> FindAll()
        {
            return _context.Comentarios
                .Include(c => c.Empleado)
                .ToList();
        }

        public Comentario FindById(int id)
        {
            return _context.Comentarios
                .Include(c => c.Empleado)
                .FirstOrDefault(c => c.Id == id);
        }

        public int Remove(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Comentario obj)
        {
            throw new NotImplementedException();
        }
    }
}
