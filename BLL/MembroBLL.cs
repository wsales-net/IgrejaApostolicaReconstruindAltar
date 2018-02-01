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

        public bool ValidarMembro(string text)
        {
            throw new NotImplementedException();
        }

        public string AddMembro(Membro membro)
        {
            throw new NotImplementedException();
        }
    }
}
