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
        public int IdPublico { get; set; }
        public int IdEndereco { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public DateTime Hora { get; set; }
        public string Descricao { get; set; }
        public int Numero { get; set; }
        public string PontoReferencia { get; set; }

        public Evento() { }
    }
}
