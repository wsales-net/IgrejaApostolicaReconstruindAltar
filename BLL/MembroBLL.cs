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

<<<<<<< HEAD
        public static bool ValidarMembro(string nome)
        {
            return MembroDAL.ValidarMembro(nome);
        }

        public static string AddMembro(Membro membro)
=======
        public bool ValidarMembro(string text)
        {
            throw new NotImplementedException();
        }

        public string AddMembro(Membro membro)
>>>>>>> ca0358fe4b87c675b99ed2d9e49d08f4a79aafb8
        {
            throw new NotImplementedException();
        }
    }
}
