using System;
using DAL;
using DTO;

namespace BLL
{
    public class MembroBLL
    {
        public static string UpdateMembro(Membro membro)
        {
            return MembroDAL.UpdateMembro(membro); ;
        }

        public static bool ValidarMembro(string nome)
        {
            return MembroDAL.ValidarMembro(nome);
        }

        public static string AddMembro(Membro membro)
        {
            string resp = MembroDAL.AddMembro(membro);
            if (string.IsNullOrEmpty(resp))
                resp = "Não foi possível cadastrar membro.";

            return resp;
        }
    }
}
