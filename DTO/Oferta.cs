using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Oferta
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public DateTime DataEntrada { get; set; }

        public Oferta() { }
    }
}
