using DAL;
using DTO;

namespace BLL
{
    public class UsuarioBLL
    {
        public static bool CheckLogin(Usuario usuario)
        {
            return UsuarioDAL.CheckLogin(usuario);
        }
    }
}
