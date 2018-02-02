using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class EnderecoBLL
    {
        public static string AddEndereco(Endereco endereco)
        {
            return EnderecoDAL.AddEndereco(endereco);
        }

        public static bool ValidarEndereco(string cep)
        {
            return EnderecoDAL.ValidarEndereco(cep);
        }

        public static Endereco GetEndereco(string cep)
        {
            return EnderecoDAL.GetEndereco(cep);
        }
    }
}
