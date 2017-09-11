using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Cidade
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public Estado Id_Estado { get; set; }

        public Cidade() { }
    }
}
