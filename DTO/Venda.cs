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
