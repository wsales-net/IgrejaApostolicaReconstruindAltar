using System;
using System.Collections.Generic;
using System.Data;
using DAL;
using DTO;

namespace BLL
{
    public class CidadeBLL
    {
        public static List<Cidade> GetCidades(string id)
        {
            var cidades = CidadeDAL.GetCidades(id);
            return cidades;
        }

        public static Cidade GetCidade(string id)
        {
            var cidades = CidadeDAL.GetCidade(id);
            return cidades;
        }
    }
}
