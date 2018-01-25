using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace DAL
{
    public class ProdutoDAL : Conecta
    {
        public static string AddProduto(Produto produto)
        {
            string sql, resp = "";
            int retorno = 0;

            try
            {
                sql = @"INSERT INTO Produto (id_categoria, nome, descricao, valor)
                        VALUES (@id_categoria, @nome, @descricao, @valor)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_categoria", produto.IdCategoria);
                cmd.Parameters.AddWithValue("@nome", produto.Nome);
                cmd.Parameters.AddWithValue("@descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@valor", produto.Valor);

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

        public static DataTable FindAllProdutos(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
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
                MessageBox.Show("Falha ao consultar - FindAllProdutos()\n\n" + ex.ToString(), "ERRO \nContato o Administrador!" +
                    "(11) 2636-5659", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        public static Produto FindProdutoById(int id)
        {
            string sql;
            Produto produto = new Produto();

            try
            {
                sql = "SELECT * FROM Produto WHERE id_produto = @id_produto";

                cmd = GetConexao().CreateCommand();
                cmd.Parameters.AddWithValue("@id_produto", id);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                if (dr.Read()) //Indicado que achou um registro
                {
                    produto.Id = int.Parse(dr["id_produto"].ToString());
                    produto.Nome = dr["nome"].ToString();
                    produto.IdCategoria = Convert.ToInt32(dr["id_categoria"].ToString());
                    produto.Descricao = dr["descricao"].ToString();
                    produto.Valor = double.Parse(dr["valor"].ToString());
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
            return produto;
        }

        public static string UpdateProduto(Produto produto)
        {
            string sql, resp = "";
            int retorno;

            try
            {
                sql = @"UPDATE Produto 
                        SET nome = @nome, descricao = @descricao, 
                        valor = @valor, id_categoria = @id_categoria 
                        WHERE(id_produto = @id_produto)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@nome", produto.Nome);
                cmd.Parameters.AddWithValue("@descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@valor", produto.Valor);
                cmd.Parameters.AddWithValue("@id_categoria", produto.Id);
                cmd.Parameters.AddWithValue("@id_produto", produto.Id);
                retorno = cmd.ExecuteNonQuery();

                if (retorno > 0)
                    resp = "Produto atualizado com sucesso.";
                else
                    resp = "Produto não atualizado.";

                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("ERRO: " + ex.ToString());
            }
            return resp;
        }

        public static string DelProduto(string id)
        {
            string sql, resp = "";
            int retorno;
            try
            {
                sql = "DELETE FROM Produto WHERE id_produto = @id_produto";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_produto", id);

                retorno = cmd.ExecuteNonQuery();
                if (retorno > 0)
                    resp = "Produto excluido com sucesso.";
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

        public static bool ValidarProduto(string nome)
        {
            bool resp = false;
            string sql;

            try
            {
                sql = "SELECT * FROM Produto WHERE nome = @nome";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@nome", nome);
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
                MessageBox.Show("ERRO: " + ex.ToString());
            }
            return resp;
        }

        public static List<Produto> GetProdutos()
        {
            List<Produto> lista = new List<Produto>();
            string sql;

            try
            {
                sql = "SELECT * FROM Produto";
                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Produto produto = new Produto();
                    produto.Id = int.Parse(dr["id_produto"].ToString());
                    produto.Nome = dr["nome"].ToString();
                    lista.Add(produto);
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

    }
}
