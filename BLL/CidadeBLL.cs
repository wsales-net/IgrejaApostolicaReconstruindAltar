using System;
using DAL;
using DTO;

namespace BLL
{
    public class CidadeBLL
    {
        public static Cidade GetCidades(string id)
        {
            return CidadeDAL.GetCidade(id);
        }

        public static object GetCidade(string v)
        {
            throw new NotImplementedException();
        }
    }
}
