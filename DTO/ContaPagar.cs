using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ContaPagar
    {
        public int Id { get; set; }
        public TipoContaPagar TipoContaPagar { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public bool Status { get; set; }

        public ContaPagar() { }
    }
}
