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
    public class RepositorioSeguimientoEnvio : IRepositorioSeguimientoEnvio
    {
        private ApplicationDbContext _context;
        public RepositorioSeguimientoEnvio(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Add(SeguimientoEnvio nuevo)
        {
            _context.SeguimientosEnvios.Add(nuevo);
            _context.SaveChanges();
            return nuevo.Id;
        }

        public List<SeguimientoEnvio> FindAll()
        {
            return _context.SeguimientosEnvios
                .Include(s => s.Comentario)
                .ToList();
        }

        public SeguimientoEnvio FindById(int id)
        {
            return _context.SeguimientosEnvios
                .Include(s => s.Comentario)
                .FirstOrDefault(s => s.Id == id);
        }

        public int Remove(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(SeguimientoEnvio obj)
        {
            throw new NotImplementedException();
        }
    }
}
