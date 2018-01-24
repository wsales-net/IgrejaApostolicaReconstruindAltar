namespace DTO
{
    public class ItemVenda
    {
        public int Id { get; set; }
        public int IdVenda { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
        public double Valor { get; set; }

        public ItemVenda() { }
    }
}
