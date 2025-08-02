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
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private ApplicationDbContext _context;
        public RepositorioUsuario(ApplicationDbContext context)
        {
            _context = context;
        }
        public int Add(Usuario nuevo)
        {
            _context.Usuarios.Add(nuevo);
            _context.SaveChanges();
            return nuevo.Id;

        }

        public List<Usuario> FindAll()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario FindByEmail(string email)
        {
            return _context.Usuarios.Where(u => u.Email == email).SingleOrDefault();
        }

        public Usuario FindById(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public int Remove(int id)
        {
            Usuario buscado = _context.Usuarios.Find(id);
            _context.Usuarios.Remove(buscado);
            _context.SaveChanges();
            return buscado.Id;
        }

        public int Update(Usuario obj)
        {
            _context.Update(obj);
            _context.SaveChanges();
            return obj.Id;
        }

        public void Detach(Usuario usuario)
        {
            var entry = _context.Entry(usuario);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
