using System;

namespace DTO
{
    public class ContaPagar
    {
        public int Id { get; set; }
        public int IdTipoContaPagar { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public bool Status { get; set; }

        public ContaPagar() { }
    }
}
