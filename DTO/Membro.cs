using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Membro
    {
        public int Id { get; set; }
        public int Id_Funcao { get; set; }
        public int Id_Endereco { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public DateTime DataNasc { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public DateTime DataRegistro { get; set; }
        public bool Status { get; set; }
        public string Foto { get; set; }

        public Membro() { }
    }
}
