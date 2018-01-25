using DTO;
using System.Data.OleDb;

namespace DAL
{
    public class UsuarioDAL : Conecta
    {
        public static bool ValidaLogin(Usuario usuario)
        {
            bool resp = false;

            try
            {
                string sql = "SELECT * FROM usuario WHERE usuario = @usuario and senha = @senha";
                //SqlConnection

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@usuario", usuario.Login);
                cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                    resp = true;
                else
                    resp = false;

                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MensagemErroBanco(ex, "ValidaLogin()");
            }
            return resp;
        }

        public static bool ValidaUsuario(Usuario usuario)
        {
            string sql;
            bool resp = false;

            try
            {
                sql = @"SELECT U.usuario, P.nome
                        FROM Usuario U
                        INNER JOIN Pessoa P ON U.id_usuario = P.id_pessoa
                        WHERE  P.nome = @nome";
                /**
	                SELECT ID_Usuario, Nome_Usuario, Login_Usuario, Senha_Usuario, Data_Inclusao, Ativo_Usuario
	                FROM Usuario U WITH(NOLOCK)
	                WHERE Login_Usuario COLLATE Latin1_General_CS_AS = @Login_Usuario COLLATE Latin1_General_CS_AS
	                AND Senha_Usuario COLLATE Latin1_General_CS_AS = @Senha_Usuario COLLATE Latin1_General_CS_AS 
	                AND Ativo_Usuario = 1
                 */

                //SqlConnection
                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@nome", usuario.Pessoa.Nome);
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                    resp = true;
                else
                    resp = false;

                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MensagemErroBanco(ex, "ValidaUsuario()");
            }
            return resp;
        }

        public static string AddUsuario(Usuario usuario)
        {
            string sql;
            string resp = "";

            try
            {
                
                sql = @"INSERT INTO usuario (id_pessoa, usuario, senha)
                        VALUES (@id_pessoa, @usuario, @senha)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_pessoa", usuario.Pessoa.Id);
                cmd.Parameters.AddWithValue("@usuario", usuario.Login);
                cmd.Parameters.AddWithValue("@senha", usuario.Senha);

                int retorno = cmd.ExecuteNonQuery();

                if (retorno > 0)
                    resp = "Cadastrado com sucesso.";
                else
                    resp = "Cadastro não realizado.";

                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MensagemErroBanco(ex, "AddUsuario");
            }
            return resp;
        }

        public static string UpdateSenha(Usuario usuario)
        {
            string sql, resp = "";
            int retorno;

            try
            {
                sql = @"UPDATE Usuario
                        SET senha = @senha
                        WHERE (usuario = @usuario)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@usuario", usuario.Login);

                retorno = cmd.ExecuteNonQuery();

                if (retorno > 0)
                    resp = "Senha atualizada com sucesso.";
                else
                    resp = "Senha não atualizada.";

                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MensagemErroBanco(ex, "UpdateSenha");
            }
            return resp;
        }
    }
}
