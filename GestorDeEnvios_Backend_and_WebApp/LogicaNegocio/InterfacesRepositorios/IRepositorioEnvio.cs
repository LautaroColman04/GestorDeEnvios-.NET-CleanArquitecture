using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
using LogicaNegocio.Enumerados.EnvioEnums;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioEnvio : IRepositorio<Envio>
    {
        List<Envio> FindAllEnProceso();
        Envio FindByNumTracking(int numTracking);
        List<Envio> FindAllByUser(int userId);
        List<Envio> FindAllByUserAndDate(int userId, DateTime fecha1, DateTime fecha2);
        List<Envio> FindAllByUserAndDateAndStatus(int userId, DateTime fecha1, DateTime fecha2, EstadoEnvio estado);
        List<Envio> FindAllByUserCommentary(int userId, string comentario);
    }
}
