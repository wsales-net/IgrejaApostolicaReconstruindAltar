using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Venda
    {
        public int Id { get; set; }
        public int IdPessoa { get; set; }
        public double Valor { get; set; }
        public double Desconto { get; set; }
        public double ValorPago { get; set; }

        public Venda() { }
    }
}
