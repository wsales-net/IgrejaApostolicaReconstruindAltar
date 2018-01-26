using DAL;
using DTO;
using System.Collections.Generic;

namespace BLL
{
    public class EstadoBLL
    {
        public static List<Estado> GetEstado()
        {
            return EstadoDAL.GetEstado();
        }
    }
}
