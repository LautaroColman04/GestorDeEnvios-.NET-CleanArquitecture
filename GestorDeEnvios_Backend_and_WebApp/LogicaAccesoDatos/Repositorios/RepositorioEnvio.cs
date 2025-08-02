using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enumerados.EnvioEnums;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioEnvio : IRepositorioEnvio
    {
        private ApplicationDbContext _context;
        public RepositorioEnvio(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Add(Envio nuevo)
        {
            _context.Envios.Add(nuevo);
            _context.SaveChanges();
            return nuevo.Id;
        }

        public List<Envio> FindAll()
        {
            return _context.Envios.
                Include(e => e.Empleado)
                .Include(e => e.Cliente)
                .Include(e => e.Seguimientos)
                .ThenInclude(s => s.Comentario)
                .ThenInclude(s => s.Empleado)
                .Include(e => (e as Comun).Agencia)
                .ToList();
        }

        public List<Envio> FindAllEnProceso()
        {
            return _context.Envios.
                Include(e => e.Empleado)
                .Include(e => e.Cliente)
                .Include(e => e.Seguimientos)
                .ThenInclude(s => s.Comentario)
                .ThenInclude(s => s.Empleado)
                .Include(e => (e as Comun).Agencia)
                .Where(e => e.Estado == LogicaNegocio.Enumerados.EnvioEnums.EstadoEnvio.EnProceso)
                .ToList();
        }

        public Envio FindById(int id)
        {
            return _context.Envios
                .Include(e => e.Empleado)
                .Include(e => e.Cliente)
                .Include(e => e.Seguimientos)
                .ThenInclude(s => s.Comentario)
                .ThenInclude(s => s.Empleado)
                .Include(e => (e as Comun).Agencia)
                .FirstOrDefault(e => e.Id == id);
        }

        public int Remove(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Envio obj)
        {
            _context.Envios.Update(obj);
            _context.SaveChanges();
            return obj.Id;
        }

        public Envio FindByNumTracking(int numTracking)
        {
            return _context.Envios
                .Include(e => e.Empleado)
                .Include(e => e.Cliente)
                .Include(e => e.Seguimientos)
                .ThenInclude(s => s.Comentario)
                .ThenInclude(s => s.Empleado)
                .Include(e => (e as Comun).Agencia)
                .FirstOrDefault(e => e.NumTracking == numTracking);
        }

        public List<Envio> FindAllByUser(int userId)
        {
            return _context.Envios
                .Include(e => e.Empleado)
                .Include(e => e.Cliente)
                .Include(e => e.Seguimientos)
                .ThenInclude(s => s.Comentario)
                .ThenInclude(s => s.Empleado)
                .Include(e => (e as Comun).Agencia)
                .Where(e => e.Cliente.Id == userId)
                .ToList(); 
        }

        public List<Envio> FindAllByUserAndDate(int userId, DateTime fecha1, DateTime fecha2)
        {
                return _context.Envios
                .Include(e => e.Empleado)
                .Include(e => e.Cliente)
                .Include(e => e.Seguimientos)
                .ThenInclude(s => s.Comentario)
                .ThenInclude(s => s.Empleado)
                .Include(e => (e as Comun).Agencia)
                .Where(e => e.Cliente.Id == userId && e.FechaCreacion > fecha1.Date && e.FechaCreacion <= fecha2.Date.AddDays(1))
                .ToList();
        }

        public List<Envio> FindAllByUserAndDateAndStatus(int userId, DateTime fecha1, DateTime fecha2, EstadoEnvio estado)
        {
                return _context.Envios
                    .Include(e => e.Empleado)
                    .Include(e => e.Cliente)
                    .Include(e => e.Seguimientos)
                    .ThenInclude(s => s.Comentario)
                    .ThenInclude(s => s.Empleado)
                    .Include(e => (e as Comun).Agencia)
                    .Where(e => e.Cliente.Id == userId && e.FechaCreacion > fecha1.Date && e.FechaCreacion <= fecha2.Date.AddDays(1) && e.Estado == estado)
                    .ToList();
        }

        public List<Envio> FindAllByUserCommentary(int userId, string comentario)
        {
            return _context.Envios
                .Include(e => e.Empleado)
                .Include(e => e.Cliente)
                .Include(e => e.Seguimientos)
                .ThenInclude(s => s.Comentario)
                .ThenInclude(s => s.Empleado)
                .Include(e => (e as Comun).Agencia)
                .Where(e => e.Cliente.Id == userId && e.Seguimientos.Any(s => s.Comentario.Texto.Contains(comentario)))
                .ToList();
        }
    }
}
