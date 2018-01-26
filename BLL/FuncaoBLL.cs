using DAL;
using System.Collections;

namespace BLL
{
    public class FuncaoBLL
    {
        public static ArrayList GetFuncao()
        {
            return FuncaoDAL.GetFuncao();
        }
    }
}
