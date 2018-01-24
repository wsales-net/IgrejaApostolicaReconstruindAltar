using DTO;
using System.Data.OleDb;
using System.Windows.Forms;

namespace DAL
{
    public class UsuarioDAL : Conecta
    {
        public static bool ValidaLogin(Usuario usuario)
        {
            bool resp;
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
                MessageBox.Show("Falha ao consultar\n" + ex.ToString(), "ERRO \nContato o Administrador!" +
                    "(11) 2636-5659", MessageBoxButtons.OK, MessageBoxIcon.Error);
                resp = false;
            }
            return resp;
        }

        public static bool validaUsuario(Usuario usuario)
        {
            string sql;
            bool resp;
            try
            {
                sql = @"SELECT U.usuario, P.nome
                        FROM Usuario U
                        INNER JOIN Pessoa P ON U.id_usuario = P.id_pessoa
                        WHERE  P.nome = @nome";

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
                MessageBox.Show("Falha ao consultar\n" + ex.ToString(), "ERRO \nContato o Administrador!" +
                    "(11) 2636-5659", MessageBoxButtons.OK, MessageBoxIcon.Error);
                resp = false;
            }
            return resp;
        }

        public static string gravarUsuario(Usuario usuario)
        {
            string sql;
            string resp;
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
                resp = "ERRO: " + ex.ToString();
            }
            return resp;
        }

        public static string alterarSenha(Usuario usuario)
        {
            string sql, resp;
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
                resp = "ERRO: " + ex.ToString();
            }
            return resp;
        }
    }
}
