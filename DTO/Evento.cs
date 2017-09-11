using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Evento
    {
        public int Id { get; set; }
        public Publico Id_Publico { get; set; }
        public Endereco Id_Endereco { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public DateTime Hora { get; set; }
        public string Descricao { get; set; }
        public int Numero { get; set; }
        public string Ponto_Referencia { get; set; }

        public Evento() { }
    }
}
