using DTO;
using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace DAL
{
    public class FuncaoDAL : Conecta
    {
        public static Funcao GetFuncao(int id)
        {
            string sql;
            Funcao funcao = new Funcao();

            try
            {
                
                sql = "SELECT * FROM Funcao WHERE id_funcao = @id";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id", id);
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    funcao.Id = Convert.ToInt32(dr["id_funcao"].ToString());
                    funcao.Descricao = dr["descricao"].ToString();
                }

                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("ERRO: " + ex.ToString());
            }
            return funcao;

        }

        public static string AddFuncao(Funcao funcao)
        {
            string sql, resp;
            int retorno;

            try
            {
                
                sql = @"INSERT INTO Funcao (id_funcao, descricao)
                        VALUES (@id_funcao, @descricao)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_funcao", funcao.Id);
                cmd.Parameters.AddWithValue("@descricao", funcao.Descricao);

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

        public static ArrayList GetFuncao()
        {
            ArrayList lista = new ArrayList();
            string sql;

            try
            {
                sql = "SELECT * FROM funcao";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Funcao funcao = new Funcao();
                    funcao.Id = Convert.ToInt32(dr["id_funcao"].ToString());
                    funcao.Descricao = dr["descricao"].ToString();

                    lista.Add(funcao);
                }

                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Falha ao consultar\n" + ex.ToString(), "ERRO \nContato o Administrador!" +
                    "(11) 2636-5659", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lista;
        }

        public static bool ValidarFuncao(string descricao)
        {
            bool resp = false;
            string sql;
            try
            {
                
                sql = "SELECT * FROM Funcao WHERE descricao = @descricao";

                cmd = GetConexao().CreateCommand();
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

        public static bool GetFuncao(string id)
        {
            bool resp = false;
            string sql;

            try
            {
                sql = @"SELECT Pessoa.id_pessoa, Pessoa.nome, Funcao.id_funcao, Funcao.descricao
                        FROM (Funcao INNER JOIN
                        Pessoa ON Funcao.id_funcao = Pessoa.id_funcao)
                        WHERE (Funcao.id_funcao = @id_funcao)";

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

        public static string UpdateFuncao(Funcao funcao)
        {
            string sql, resp;
            int retorno;

            try
            {
                
                sql = @"UPDATE Funcao 
                        SET descricao = @descricao 
                        WHERE (id_funcao = @id_funcao)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@descricao", funcao.Descricao);
                cmd.Parameters.AddWithValue("@id_funcao", funcao.Id);

                retorno = cmd.ExecuteNonQuery();

                if (retorno > 0)
                    resp = "Função atualizada com sucesso.";
                else
                    resp = "Função não atualizada.";

                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                resp = "ERRO: " + ex.ToString();
            }
            return resp;
        }

        public static string DelFuncao(string id)
        {
            string sql, resp;
            int retorno;

            try
            {
                
                sql = "DELETE FROM Funcao WHERE id_funcao = @id_funcao";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_funcao", id);

                retorno = cmd.ExecuteNonQuery();
                if (retorno > 0)
                    resp = "Função excluida com sucesso.";
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

        public static DataTable FindAllFuncao()
        {
            DataTable dt = new DataTable();
            string sql;
            try
            {
                
                sql = @"SELECT id_funcao as Código, descricao as Descrição
                        FROM Funcao 
                        ORDER BY id_funcao";

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

        public static Funcao FindFuncaoById(string id)
        {
            string sql;
            Funcao funcao = new Funcao();

            try
            {
                
                sql = "SELECT * FROM Funcao WHERE id_funcao = @id_funcao";

                cmd = GetConexao().CreateCommand();
                cmd.Parameters.AddWithValue("@id_funcao", id);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                if (dr.Read()) //Indicado que achou um registro
                {
                    funcao.Id = Convert.ToInt32(dr["id_funcao"].ToString());
                    funcao.Descricao = dr["descricao"].ToString();
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
            return funcao;
        }

        public static int GetRegistros()
        {
            string sql;
            int id = 0;

            try
            {
                
                sql = "SELECT MAX(id_funcao) AS registros FROM Funcao";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    id = Convert.ToInt32(dr["registros"].ToString());
                }

                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("ERRO: " + ex.ToString());
            }
            return id;
        }

    }
}
