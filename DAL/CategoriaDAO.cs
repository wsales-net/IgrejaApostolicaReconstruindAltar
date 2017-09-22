using DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{

    public class CategoriaDAO
    {
        private OleDbConnection conexao;
        private OleDbCommand cmd;
        private OleDbDataReader dr;

        public DataTable FindAll()
        {
            DataTable dt = new DataTable();
            string sql = "";
            try
            {
                conexao = Conecta.getConexao();
                sql = "SELECT id_categoria as Código, descricao as Descrição FROM Categoria ORDER BY id_categoria";
                cmd = conexao.CreateCommand();
                cmd.CommandText = sql;

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                dt.BeginLoadData();
                da.Fill(dt);
                dt.EndLoadData();

                cmd.Dispose();
                conexao.Close();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Falha ao consultar - FindAll()\n\n" + ex.ToString(), "ERRO \nContato o Administrador!" +
                    "(11) 2636-5659", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        public Categoria FindById(string id)
        {
            string sql = "";
            Categoria cat = new Categoria();
            try
            {
                conexao = Conecta.getConexao();
                sql = "SELECT * FROM Categoria WHERE id_categoria = @id_categoria";

                cmd = conexao.CreateCommand();
                cmd.Parameters.AddWithValue("@id_categoria", id);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                if (dr.Read()) //Indicado que achou um registro
                {
                    cat.Id = int.Parse(dr["id_categoria"].ToString());
                    cat.Descricao = dr["descricao"].ToString();
                }
                dr.Dispose();
                cmd.Dispose();
                conexao.Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Falha ao consultar - FindById()\n\n" + ex.ToString(), "ERRO \nContato o Administrador!" +
                    "(11) 2636-5659", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return cat;
        }

        public Categoria FindByDesc(string desc)
        {
            string sql = "";
            Categoria cat = new Categoria();
            try
            {
                conexao = Conecta.getConexao();
                sql = "SELECT descricao FROM Categoria WHERE descricao = @descricao";

                cmd = conexao.CreateCommand();
                cmd.Parameters.AddWithValue("@descricao", desc);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                if (dr.Read()) //Indicado que achou um registro
                {
                    cat.Id = int.Parse(dr["id_categoria"].ToString());
                    cat.Descricao = dr["descricao"].ToString();
                }
                dr.Dispose();
                cmd.Dispose();
                conexao.Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Falha ao consultar - FindById()\n\n" + ex.ToString(), "ERRO \nContato o Administrador!" +
                    "(11) 2636-5659", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return cat;
        }

        public string cadastrarCategoria(Categoria cat)
        {
            string sql = "";
            int retorno = 0;
            string resp = "";

            try
            {
                conexao = Conecta.getConexao();
                sql = "INSERT INTO Categoria (id_categoria, descricao) ";
                sql += "VALUES (@id_categoria, @descricao)";

                cmd = conexao.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_categoria", cat.Id);
                cmd.Parameters.AddWithValue("@descricao", cat.Descricao);

                retorno = cmd.ExecuteNonQuery();

                if (retorno > 0)
                    resp = "Cadastrado com sucesso.";
                else
                    resp = "Cadastro não realizado.";

                cmd.Dispose();
                conexao.Dispose();
            }
            catch (OleDbException ex)
            {
                resp = "ERRO: " + ex.ToString();
            }
            return resp;
        }

        public string excluirCategoria(string id)
        {
            string sql;
            int retorno;
            string resp = "";
            try
            {
                conexao = Conecta.getConexao();
                sql = "DELETE FROM Categoria WHERE id_categoria = @id_categoria";

                cmd = conexao.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_categoria", id);

                retorno = cmd.ExecuteNonQuery();
                if (retorno > 0)
                    resp = "Categoria excluida com sucesso.";
                else
                    resp = "Exclusão não realizada.";

                cmd.Dispose();
                conexao.Dispose();
            }
            catch (OleDbException ex)
            {
                resp = "ERRO: " + ex.ToString();
            }
            return resp;
        }

        public string alterarCategoria(Categoria cat)
        {
            string sql, resp = "";
            int retorno;

            try
            {
                conexao = Conecta.getConexao();
                sql = "UPDATE Categoria ";
                sql += "SET descricao = @descricao ";
                sql += "WHERE (id_categoria = @id_categoria)";

                cmd = conexao.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@descricao", cat.Descricao);
                cmd.Parameters.AddWithValue("@id_categoria", cat.Id);

                retorno = cmd.ExecuteNonQuery();

                if (retorno > 0)
                    resp = "Categoria atualizada com sucesso.";
                else
                    resp = "Categoria não atualizada.";

                cmd.Dispose();
                conexao.Dispose();
            }
            catch (OleDbException ex)
            {
                resp = "ERRO: " + ex.ToString();
            }
            return resp;
        }

        public bool validarCategoria(string descricao)
        {
            bool resp = false;
            string sql = "";
            try
            {
                conexao = Conecta.getConexao();
                sql = "SELECT * FROM Categoria WHERE descricao = @descricao";

                cmd = conexao.CreateCommand();
                cmd.Parameters.AddWithValue("@descricao", descricao);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                if (dr.HasRows) //Indicado que achou um registro
                    resp = true;
                else
                    resp = false;

                dr.Dispose();
                cmd.Dispose();
                conexao.Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Falha ao consultar - buscarCategoria()\n\n" + ex.ToString(), "ERRO \nContato o Administrador!" +
                    "(11) 2636-5659", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return resp;
        }

        public bool buscarCategoria(string id)
        {
            bool resp = false;
            string sql = "";
            try
            {
                conexao = Conecta.getConexao();
                sql = "SELECT Produto.id_produto, Produto.nome, Categoria.id_categoria, Categoria.descricao ";
                sql += "FROM (Categoria INNER JOIN ";
                sql += "Produto ON Categoria.id_categoria = Produto.id_categoria) ";
                sql += "WHERE (Categoria.id_categoria = @id_categoria)";

                cmd = conexao.CreateCommand();
                cmd.Parameters.AddWithValue("@id_categoria", id);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                if (dr.HasRows) //Indicado que achou um registro
                    resp = true;
                else
                    resp = false;

                dr.Dispose();
                cmd.Dispose();
                conexao.Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Falha ao consultar - buscarCategoriaP()\n\n" + ex.ToString(), "ERRO \nContato o Administrador!" +
                    "(11) 2636-5659", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return resp;
        }

        public List<Categoria> obterCategoria()
        {
            List<Categoria> lista = new List<Categoria>();
            string sql;
            try
            {
                sql = "SELECT * FROM Categoria ORDER BY Descricao";
                conexao = Conecta.getConexao();
                cmd = conexao.CreateCommand();
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Categoria cat = new Categoria();
                    cat.Id = int.Parse(dr["id_categoria"].ToString());
                    cat.Descricao = dr["descricao"].ToString();
                    lista.Add(cat);
                }

                dr.Dispose();
                cmd.Dispose();
                conexao.Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Falha ao consultar - obterCategoria()\n\n" + ex.ToString(), "ERRO \nContato o Administrador!" +
                    "(11) 2636-5659", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lista;
        }

        public ArrayList getCategoria()
        {
            ArrayList lista = new ArrayList();
            string sql = "";
            try
            {
                sql = "SELECT * FROM categoria";
                conexao = Conecta.getConexao();

                cmd = conexao.CreateCommand();
                cmd.CommandText = sql;
                OleDbDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Categoria cat = new Categoria();
                    cat.Id = int.Parse(dr["id_categoria"].ToString());
                    cat.Descricao = dr["descricao"].ToString();

                    lista.Add(cat);
                }

                dr.Dispose();
                cmd.Dispose();
                conexao.Dispose();
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
