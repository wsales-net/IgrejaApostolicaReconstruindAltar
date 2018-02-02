using DTO;
using System.Collections;
using System.Data.OleDb;
using System.Windows.Forms;

namespace DAL
{
    public class PublicoDAL : Conecta
    {
        public static string AddPublico(string funcao)
        {
            string sql, resp = "";
            int retorno = 0;

            try
            {
                sql = @"INSERT INTO Funcao (descricao)
                        VALUES (@descricao)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@descricao", funcao);

                retorno = cmd.ExecuteNonQuery();

                if (retorno > 0)
                    resp = "Cadastrado com sucesso.";
                else
                    resp = "Cadastro não realizado.";

                //funcao.Id = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MensagemErroBanco(ex, "AddPublico()");
            }
            return resp;
        }

        public static ArrayList GetPublico()
        {
            ArrayList lista = new ArrayList();
            string sql;

            try
            {
                sql = "SELECT * FROM Publico";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Publico publico = new Publico();
                    publico.Id = int.Parse(dr["id_publico"].ToString());
                    publico.Descricao = dr["descricao"].ToString();
                    publico.Classificacao = dr["classificacao"].ToString();

                    lista.Add(publico);
                }

                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MensagemErroBanco(ex, "GetPublico()");
            }
            return lista;
        }

        public static Publico GetPublico(int id)
        {
            string sql;
            Publico publico = new Publico();

            try
            {
                sql = "SELECT * FROM Publico WHERE id_publico = @id";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id", id);
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    publico.Id = int.Parse(dr["id_publico"].ToString());
                    publico.Descricao = dr["descricao"].ToString();
                }

                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MensagemErroBanco(ex, "GetPublico()");
            }
            return publico;
        }
    }
}
