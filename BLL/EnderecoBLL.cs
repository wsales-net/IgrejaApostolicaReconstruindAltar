using System;
using DAL;
using DTO;

namespace BLL
{
    public class EnderecoBLL
    {
        public static bool ValidarEndereco(string cep)
        {
            return EnderecoDAL.ValidarEndereco(cep);
        }

        public static Endereco GetEndereco(string cep)
        {
            return EnderecoDAL.GetEndereco(cep);
        }

        public static string AddEndereco(Endereco endereco)
        {
            string resp = EnderecoDAL.AddEndereco(endereco);
            if (string.IsNullOrEmpty(resp))
                resp = "Não foi possível cadastrar endereço.";

            return resp;
        }
    }
}
