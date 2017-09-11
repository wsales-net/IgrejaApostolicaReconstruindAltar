using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ContaReceber
    {
        public int Id { get; set; }
        public int IdVenda { get; set; }
        public StatusPagamento StatusPagamento { get; set; }
        public DateTime DataCompra { get; set; }
        public DateTime? DataPagamento { get; set; }
        public DateTime DataVencimento { get; set; }

        public ContaReceber() { }
    }
}
