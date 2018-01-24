using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace DAL
{
    public class MembroDAL : Conecta
    {
        public static string AddMembro(Membro membro)
        {
            string sql, resp;
            int retorno = 0;

            try
            {
                
                sql = @"INSERT INTO Pessoa (id_funcao, id_endereco, nome, sexo, dataNascimento, telefone,
                        celular, numero, complemento, email, rg, cpf, dataRegistro, status, foto)
                        VALUES (@id_funcao, @id_endereco, @nome, @sexo, @dataNascimento, @telefone,
                        @celular, @numero, @complmento, @email, @rg, @cpf, @dataRegistro, @status, @foto)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_funcao", membro.IdFuncao);
                cmd.Parameters.AddWithValue("@id_endereco", membro.IdEndereco);
                cmd.Parameters.AddWithValue("@nome", membro.Nome);
                cmd.Parameters.AddWithValue("@sexo", membro.Sexo);
                cmd.Parameters.AddWithValue("@dataNascimento", membro.DataNasc);
                cmd.Parameters.AddWithValue("@telefone", membro.Telefone);
                cmd.Parameters.AddWithValue("@celular", membro.Celular);
                cmd.Parameters.AddWithValue("@numero", membro.Numero);
                cmd.Parameters.AddWithValue("@complemento", membro.Complemento);
                cmd.Parameters.AddWithValue("@email", membro.Email);
                cmd.Parameters.AddWithValue("@rg", membro.Rg);
                cmd.Parameters.AddWithValue("@cpf", membro.Cpf);
                cmd.Parameters.AddWithValue("@dataRegistro", membro.DataRegistro);
                cmd.Parameters.AddWithValue("@status", membro.Status);
                cmd.Parameters.AddWithValue("@foto", membro.Foto);

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

        public static List<Membro> GetMembros()
        {
            List<Membro> lista = new List<Membro>();
            string sql;

            try
            {
                sql = "SELECT * FROM Pessoa";
                
                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Membro pessoa = new Membro();
                    pessoa.Id = Convert.ToInt32(dr["id_pessoa"].ToString());
                    pessoa.Nome = dr["Nome"].ToString();
                    lista.Add(pessoa);
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

        public static Membro GetMembro(string id)
        {
            string sql;
            Membro membro = new Membro();

            try
            {
                sql = "SELECT * FROM Pessoa WHERE id_pessoa = @id";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id", id);
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    membro.Id = Convert.ToInt32(dr["id_pessoa"].ToString());
                    membro.Nome = dr["nome"].ToString();
                }

                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("ERRO: " + ex.ToString());
            }
            return membro;
        }

        public static bool ValidarMembro(string nome)
        {
            bool resp = false;
            string sql;

            try
            {
                sql = "SELECT * FROM Pessoa WHERE nome = @nome";

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

        public static Membro FindMembroById(int id)
        {
            string sql;
            Membro membro = new Membro();

            try
            {
                sql = "SELECT * FROM Pessoa WHERE id_pessoa = @id_pessoa";

                cmd = GetConexao().CreateCommand();
                cmd.Parameters.AddWithValue("@id_pessoa", id);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                if (dr.HasRows) //Indicado que achou um registro
                {
                    membro.Id = Convert.ToInt32(dr["id_pessoa"].ToString());
                    membro.IdFuncao = Convert.ToInt32(dr["id_funcao"].ToString());
                    membro.IdEndereco = Convert.ToInt32((dr["id_endereco"].ToString()));
                    membro.Nome = dr["nome"].ToString();
                    membro.Sexo = dr["sexo"].ToString();
                    membro.Email = dr["email"].ToString();
                    membro.DataNasc = Convert.ToDateTime(dr["dataNascimento"].ToString());
                    membro.Telefone = dr["telefone"].ToString();
                    membro.Celular = dr["celular"].ToString();
                    membro.Numero = Convert.ToInt32(dr["numero"].ToString());
                    membro.Complemento = dr["complemento"].ToString();
                    membro.Rg = dr["rg"].ToString();
                    membro.Cpf = dr["cpf"].ToString();
                    membro.Status = Convert.ToBoolean(dr["status"].ToString());
                    membro.Foto = dr["foto"].ToString();
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
            return membro;
        }

        public static DataTable FindMembro(string texto)
        {
            DataTable dt = new DataTable();
            string sql;

            try
            {
                
                sql = @"SELECT id_pessoa as Código, nome as Nome
                        FROM Pessoa
                        WHERE (nome LIKE  '%" + texto + "%')";

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

        public static string UpdateMembro(Membro membro)
        {
            string sql, resp = "";
            int retorno;

            try
            {
                sql = @"UPDATE Pessoa
                        SET id_endereco = @id_endereco, id_funcao = @id_funcao,
                        nome = @nome, sexo = @sexo, dataNascimento = @dataNascimento,
                        telefone = @telefone, celular = @celular, numero = @numero,
                        complemento = @complemento, rg = @rg, email = @email, cpf = @cpf,
                        dataRegistro = @dataRegistro, status = @status, foto = @foto
                        WHERE(id_pessoa = @id_pessoa)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_endereco", membro.IdEndereco);
                cmd.Parameters.AddWithValue("@id_funcao", membro.IdFuncao);
                cmd.Parameters.AddWithValue("@nome", membro.Nome);
                cmd.Parameters.AddWithValue("@sexo", membro.Sexo);
                cmd.Parameters.AddWithValue("@dataNascimento", membro.DataNasc);
                cmd.Parameters.AddWithValue("@telefone", membro.Telefone);
                cmd.Parameters.AddWithValue("@celular", membro.Celular);
                cmd.Parameters.AddWithValue("@numero", membro.Numero);
                cmd.Parameters.AddWithValue("@complemento", membro.Complemento);
                cmd.Parameters.AddWithValue("@rg", membro.Rg);
                cmd.Parameters.AddWithValue("@email", membro.Email);
                cmd.Parameters.AddWithValue("@cpf", membro.Cpf);
                cmd.Parameters.AddWithValue("@dataRegistro", membro.DataRegistro);
                cmd.Parameters.AddWithValue("@status", membro.Status);
                cmd.Parameters.AddWithValue("@foto", membro.Foto);
                cmd.Parameters.AddWithValue("@id_pessoa", membro.Id);
                retorno = cmd.ExecuteNonQuery();

                if (retorno > 0)
                    resp = "Membro atualizado com sucesso.";
                else
                    resp = "Membro não atualizado.";

                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("ERRO: " + ex.ToString());
            }
            return resp;
        }
    }
}
