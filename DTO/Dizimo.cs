using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Dizimo
    {
        public int Id { get; set; }
        public Membro IdMembro { get; set; }
        public double Valor { get; set; }
        public DateTime DataEntrada { get; set; }
        public string Observacao { get; set; }

        public Dizimo() { }
    }
}
