using DAL;
using System.Data;

namespace BLL
{
    public class ContaReceberBLL
    {
        public static DataTable FindAllContaReceber()
        {
            return ContaReceberDAL.FindAllContaReceber();
        }
    }
}
