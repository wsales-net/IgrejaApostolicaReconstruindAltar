using DTO;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace DAL
{
    class CategoriaDAL : Conecta
    {
        public static DataTable FindAllCategoria()
        {
            DataTable dt = new DataTable();
            string sql;
            try
            {
                sql = "SELECT id_categoria as Código, descricao as Descrição FROM Categoria ORDER BY id_categoria";
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
                MessageBox.Show("Falha ao consultar - FindAll()\nContato o Administrador - (11) 2636-5659\n\n" + ex.ToString(),
                    "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        public static Categoria FindCategoriaById(string id)
        {
            string sql;
            Categoria cat = new Categoria();
            try
            {
                sql = "SELECT * FROM Categoria WHERE id_categoria = @id_categoria";

                cmd = GetConexao().CreateCommand();
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
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Falha ao consultar - FindAll()\nContato o Administrador - (11) 2636-5659\n\n" + ex.ToString(),
                    "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return cat;
        }

        public static Categoria FindCategoriaByDesc(string desc)
        {
            string sql;
            Categoria cat = new Categoria();
            try
            {

                sql = "SELECT descricao FROM Categoria WHERE descricao = @descricao";

                cmd = GetConexao().CreateCommand();
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
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Falha ao consultar - FindAll()\nContato o Administrador - (11) 2636-5659\n\n" + ex.ToString(),
                    "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return cat;
        }

        public static string AddCategoria(Categoria cat)
        {
            string sql, resp;
            int retorno;

            try
            {

                sql = @"INSERT INTO Categoria (id_categoria, descricao) 
                        VALUES (@id_categoria, @descricao)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_categoria", cat.Id);
                cmd.Parameters.AddWithValue("@descricao", cat.Descricao);

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

        public static string DelCategoria(string id)
        {
            string sql, resp;
            int retorno;
            try
            {

                sql = "DELETE FROM Categoria WHERE id_categoria = @id_categoria";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_categoria", id);

                retorno = cmd.ExecuteNonQuery();
                if (retorno > 0)
                    resp = "Categoria excluida com sucesso.";
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

        public static string UpdateCategoria(Categoria cat)
        {
            string sql, resp;
            int retorno;

            try
            {

                sql = @"UPDATE Categoria 
                        SET descricao = @descricao 
                        WHERE(id_categoria = @id_categoria)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@descricao", cat.Descricao);
                cmd.Parameters.AddWithValue("@id_categoria", cat.Id);

                retorno = cmd.ExecuteNonQuery();

                if (retorno > 0)
                    resp = "Categoria atualizada com sucesso.";
                else
                    resp = "Categoria não atualizada.";

                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                resp = "ERRO: " + ex.ToString();
            }
            return resp;
        }

        public static bool ValidarCategoria(string descricao)
        {
            bool resp = false;
            string sql;

            try
            {

                sql = "SELECT * FROM Categoria WHERE descricao = @descricao";

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
                MessageBox.Show("Falha ao consultar - FindAll()\nContato o Administrador - (11) 2636-5659\n\n" + ex.ToString(),
                    "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return resp;
        }

        public static bool GetCategoria(string id)
        {
            bool resp = false;
            string sql;

            try
            {

                sql = @"SELECT Produto.id_produto, Produto.nome, Categoria.id_categoria, Categoria.descricao
                        FROM (Categoria INNER JOIN
                        Produto ON Categoria.id_categoria = Produto.id_categoria)
                        WHERE (Categoria.id_categoria = @id_categoria)";

                cmd = GetConexao().CreateCommand();
                cmd.Parameters.AddWithValue("@id_categoria", id);
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
                MessageBox.Show("Falha ao consultar - FindAll()\nContato o Administrador - (11) 2636-5659\n\n" + ex.ToString(),
                    "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return resp;
        }

        public static List<Categoria> GetCategorias()
        {
            List<Categoria> lista = new List<Categoria>();
            string sql;
            try
            {
                sql = "SELECT * FROM Categoria ORDER BY Descricao";

                cmd = GetConexao().CreateCommand();
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
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Falha ao consultar - FindAll()\nContato o Administrador - (11) 2636-5659\n\n" + ex.ToString(),
                    "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lista;
        }

        public static ArrayList GetCategoria()
        {
            ArrayList lista = new ArrayList();
            string sql;
            try
            {
                sql = "SELECT * FROM categoria";

                cmd = GetConexao().CreateCommand();
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
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Falha ao consultar - FindAll()\nContato o Administrador - (11) 2636-5659\n\n" + ex.ToString(),
                    "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lista;
        }
    }
}
