using DAL;
using DTO;

namespace BLL
{
    public class MembroBLL
    {
        public static string UpdateMembro(Membro membro)
        {
            return MembroDAL.UpdateMembro(membro);
        }
    }
}
