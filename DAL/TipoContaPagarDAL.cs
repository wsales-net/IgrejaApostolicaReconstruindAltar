using DTO;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace DAL
{
     class TipoContaPagarDAL : Conecta
    {
        public static List<TipoContaPagar> GetTipoContaPagar()
        {
            List<TipoContaPagar> lista = new List<TipoContaPagar>();
            string sql;

            try
            {
                sql = "SELECT * FROM TipoContaPagar ORDER BY Descricao";
                
                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TipoContaPagar conta = new TipoContaPagar();
                    conta.Id = int.Parse(dr["id_tipocontapagar"].ToString());
                    conta.Descricao = dr["descricao"].ToString();
                    lista.Add(conta);
                }

                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Falha ao consultar - obterCategoria()\n\n" + ex.ToString(), "ERRO \nContato o Administrador!" +
                    "(11) 2636-5659", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lista;
        }

        //Verifica as ontas que não foram pagas
        public static List<TipoContaPagar> GetTipoContasPagas()
        {
            List<TipoContaPagar> lista = new List<TipoContaPagar>();
            string sql;
            try
            {
                sql = @"SELECT TipoContaPagar.* 
                        FROM (ContasPagar 
                        INNER JOIN TipoContaPagar 
                        ON ContasPagar.id_tipocontapagar = TipoContaPagar.id_tipocontapagar) 
                        WHERE  (ContasPagar.status = False)";
                
                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    TipoContaPagar conta = new TipoContaPagar();
                    conta.Id = int.Parse(dr["id_tipocontapagar"].ToString());
                    conta.Descricao = dr["descricao"].ToString();
                    lista.Add(conta);
                }

                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Falha ao consultar - obterCategoria()\n\n" + ex.ToString(), "ERRO \nContato o Administrador!" +
                    "(11) 2636-5659", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lista;
        }

        public static TipoContaPagar FindTipoContaById(string id)
        {
            string sql;
            TipoContaPagar tipoconta = new TipoContaPagar();

            try
            {
                
                sql = "SELECT * FROM TipoContaPagar WHERE id_tipocontapagar = @id_tipocontapagar";

                cmd = GetConexao().CreateCommand();
                cmd.Parameters.AddWithValue("@id_tipocontapagar", id);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                if (dr.Read()) //Indicado que achou um registro
                {
                    tipoconta.Id = int.Parse(dr["id_tipocontapagar"].ToString());
                    tipoconta.Descricao = dr["descricao"].ToString();
                }
                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Falha ao consultar - FindById()\n\n" + ex.ToString(), "ERRO \nContato o Administrador!" +
                    "(11) 2636-5659", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return tipoconta;
        }

        public static int GetRegistros()
        {
            string sql;
            int id = 0;

            try
            {
                
                sql = "SELECT MAX(id_tipocontapagar) AS registros FROM TipoContaPagar";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    id = int.Parse(dr["registros"].ToString());
                }

                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                System.Windows.Forms.MessageBox.Show("ERRO: " + ex.ToString());
            }
            return id;
        }

        public static string UpdateTipoPagamento(TipoContaPagar tp)
        {
            string sql, resp = "";
            int retorno;

            try
            {
                
                sql = @"UPDATE TipoContaPagar 
                        SET descricao = @descricao 
                        WHERE (id_tipocontapagar = @id_tipocontapagar)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@descricao", tp.Descricao);
                cmd.Parameters.AddWithValue("@id_tipocontapagar", tp.Id);

                retorno = cmd.ExecuteNonQuery();

                if (retorno > 0)
                    resp = "Tipo de pagamento atualizada com sucesso.";
                else
                    resp = "Tipo de pagamento não atualizada.";

                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                resp = "ERRO: " + ex.ToString();
            }
            return resp;
        }

        public static string AddTipoPagamento(TipoContaPagar tp)
        {
            string sql, resp = "";
            int retorno = 0;

            try
            {
                
                sql = @"INSERT INTO TipoContaPagar (id_tipocontapagar, descricao)
                        VALUES (@id_tipocontapagar, @descricao)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_tipocontapagar", tp.Id);
                cmd.Parameters.AddWithValue("@descricao", tp.Descricao);

                retorno = cmd.ExecuteNonQuery();

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

        public static bool ValidarTipoPagamento(string descricao)
        {
            bool resp = false;
            string sql = "";

            try
            {
                
                sql = "SELECT * FROM TipoContaPagar WHERE descricao = @descricao";

                cmd = GetConexao().CreateCommand();
                //cmd.Parameters.AddWithValue("@id_funcao", funcao.Id);
                cmd.Parameters.AddWithValue("@descricao", descricao);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                if (dr.HasRows) //Indicado que achou um registro
                    resp = true;
                else
                    resp = false;

                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Falha ao consultar - buscarCategoria()\n\n" + ex.ToString(), "ERRO \nContato o Administrador!" +
                    "(11) 2636-5659", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return resp;
        }

        public static string DelTipoPagamento(string id)
        {
            string sql;
            int retorno;
            string resp = "";

            try
            {
                
                sql = "DELETE FROM TipoContaPagar WHERE id_tipocontapagar = @id_tipocontapagar";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_tipocontapagar", id);

                retorno = cmd.ExecuteNonQuery();
                if (retorno > 0)
                    resp = "Tipo de pagamento excluído com sucesso.";
                else
                    resp = "Exclusão não realizada.";

                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                resp = "ERRO: " + ex.ToString();
            }
            return resp;
        }

        public static bool GetTipoPagamento(string id)
        {
            bool resp = false;
            string sql = "";

            try
            {
                
                sql = @"SELECT ContasPagar.id_contaspagar, ContasPagar.descricao, TipoContaPagar.id_tipocontapagar, TipoContaPagar.descricao AS TipoConta 
                        FROM (TipoContaPagar INNER JOIN 
                        ContasPagar ON TipoContaPagar.id_tipocontapagar = ContasPagar.id_tipocontapagar) 
                        WHERE (TipoContaPagar.id_tipocontapagar = id_funcao)";

                cmd = GetConexao().CreateCommand();
                cmd.Parameters.AddWithValue("@id_funcao", id);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                if (dr.HasRows) //Indicado que achou um registro
                    resp = true;
                else
                    resp = false;

                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Falha ao consultar - buscarCategoriaP()\n\n" + ex.ToString(), "ERRO \nContato o Administrador!" +
                    "(11) 2636-5659", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return resp;
        }

        public static DataTable FindAllTipoPagamento()
        {
            DataTable dt = new DataTable();
            string sql;

            try
            {
                
                sql = @"SELECT id_tipocontapagar as Código, descricao as Descrição
                        FROM TipoContaPagar 
                        ORDER BY id_tipocontapagar";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                dt.BeginLoadData();
                da.Fill(dt);
                dt.EndLoadData();

                cmd.Dispose();
                GetConexao().Close();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Falha ao consultar - FindAll()\n\n" + ex.ToString(), "ERRO \nContato o Administrador!" +
                    "(11) 2636-5659", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }
    }
}
