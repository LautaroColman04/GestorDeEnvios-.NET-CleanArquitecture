using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.VO.Agencia
{
    public class Ubicacion
    {
        public float Latitud { get; init; }
        public float Longitud { get; init; }

        public Ubicacion(float latitud, float longitud)
        {
            Latitud = latitud;
            Longitud = longitud;
            Validar();
        }
        public void Validar()
        {
            if (Latitud < -90 || Latitud > 90)
            {
                throw new ArgumentOutOfRangeException("La latitud debe estar entre -90 y 90");
            }
            if (Longitud < -180 || Longitud > 180)
            {
                throw new ArgumentOutOfRangeException("La longitud debe estar entre -180 y 180");
            }
        } 
    }
}
