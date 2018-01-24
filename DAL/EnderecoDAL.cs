using DTO;
using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace DAL
{
    public class EnderecoDAL : Conecta
    {
        public static Endereco GetEndereco(string cep)
        {
            Endereco end = new Endereco();
            string sql;
            try
            {
                sql = @"SELECT Endereco.id_endereco, Cidade.id_estado, Endereco.id_cidade, Estado.sigla, Endereco.cep, Endereco.lougradouro, Endereco.bairro
                        FROM ((Endereco INNER JOIN
                        Cidade ON Endereco.id_cidade = Cidade.id_cidade) INNER JOIN
                        Estado ON Cidade.id_estado = Estado.id_estado)
                        WHERE (Endereco.cep = @cep)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@cep", cep);
                OleDbDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    end.Id = Convert.ToInt32(dr["id_endereco"].ToString());
                    end.Cidade = CidadeDAL.GetCidade(dr["id_cidade"].ToString());
                    end.Cidade.Estado = EstadoDAL.GetEstado(dr["sigla"].ToString());
                    end.Cep = dr["cep"].ToString();
                    end.Logradouro = dr["lougradouro"].ToString();
                    end.Bairro = dr["bairro"].ToString();
                }
                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("ERRO: " + ex.ToString());
            }
            return end;
        }

        public static Endereco GetEnderecoById(string id)
        {
            Endereco end = new Endereco();
            string sql;
            try
            {
                sql = @"SELECT Endereco.id_endereco, Cidade.id_estado, Endereco.id_cidade, Estado.sigla, Endereco.cep, Endereco.lougradouro, Endereco.bairro
                        FROM ((Endereco INNER JOIN
                        Cidade ON Endereco.id_cidade = Cidade.id_cidade) INNER JOIN
                        Estado ON Cidade.id_estado = Estado.id_estado)
                        WHERE (Endereco.id_endereco = @id_endereco)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_endereco", id);
                OleDbDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    end.Id = Convert.ToInt32(dr["id_endereco"].ToString());
                    end.Cidade = CidadeDAL.GetCidade(dr["id_cidade"].ToString());
                    end.Cidade.Estado = EstadoDAL.GetEstado(dr["sigla"].ToString());
                    end.Cep = dr["cep"].ToString();
                    end.Logradouro = dr["lougradouro"].ToString();
                    end.Bairro = dr["bairro"].ToString();
                }
                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("ERRO: " + ex.ToString());
            }
            return end;
        }

        //Verifica se cep esta cadastrado no banco
        public static bool ValidarEndereco(string cep)
        {
            bool resp = false;
            string sql;
            try
            {
                sql = "SELECT * FROM Endereco WHERE cep = @cep";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@cep", cep);
                OleDbDataReader dr = cmd.ExecuteReader();

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

        public static string AddEndereco(Endereco end)
        {
            string resp, sql;
            int retorno;
            try
            {
                sql = @"INSERT INTO Endereco (id_cidade, cep, lougradouro, bairro)
                        VALUES (@id_cidade, @cep, @lougradouro, @bairro)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_cidade", end.Cidade.Id);
                cmd.Parameters.AddWithValue("@cep", end.Cep);
                cmd.Parameters.AddWithValue("@lougradouro", end.Logradouro);
                cmd.Parameters.AddWithValue("@bairro", end.Bairro);

                retorno = cmd.ExecuteNonQuery();

                if (retorno > 0)
                    resp = "Cadastrado com sucesso.";
                else
                    resp = "Endereço não cadastrado.";
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
