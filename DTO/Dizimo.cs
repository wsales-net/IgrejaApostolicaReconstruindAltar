using System;

namespace DTO
{
    public class Dizimo
    {
        public int Id { get; set; }
        public int IdMembro { get; set; }
        public double Valor { get; set; }
        public DateTime DataEntrada { get; set; }
        public string Observacao { get; set; }

        public Dizimo() { }
    }
}
