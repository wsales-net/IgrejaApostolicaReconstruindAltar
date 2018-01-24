using System;

namespace DTO
{
    public class ContaReceber
    {
        public int Id { get; set; }
        public int IdVenda { get; set; }
        public int IdFormaPagamento { get; set; }
        public DateTime DataCompra { get; set; }
        public DateTime? DataPagamento { get; set; }
        public DateTime DataVencimento { get; set; }

        public ContaReceber() { }
    }
}
