using DTO;
using System.Data.OleDb;
using System.Windows.Forms;

namespace DAL
{
    public class VendaDAL : Conecta
    {
        public static object GetTotal()
        {
            object retorno = null;
            string sql;

            try
            {
                sql = "SELECT MAX(id_venda) AS Total FROM Venda";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                if (dr.Read())
                    retorno = dr["Total"].ToString();
                else
                    retorno = -999998;

                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();

            }
            catch (OleDbException ex)
            {
                MessageBox.Show("ERRO: " + ex.ToString());
            }
            return retorno;
        }

        public static string AddVenda(Venda venda)
        {
            string sql, resp = "";
            int retorno;

            try
            {
                sql = @"INSERT INTO Venda (id_pessoa, valor, desconto, valorpago) 
                        VALUES (@id_pessoa, @valor, @desconto, @valorpago)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_pessoa", venda.IdPessoa);
                cmd.Parameters.AddWithValue("@valor", venda.Valor);
                cmd.Parameters.AddWithValue("@desconto", venda.Desconto);
                cmd.Parameters.AddWithValue("@valorpago", venda.ValorPago);

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

        public static string UpdateVenda(Venda venda)
        {
            string sql, resp = "";
            int retorno;

            try
            {
                sql = @"UPDATE Venda 
                        SET id_pessoa = @id_pessoa, valor = @valor, 
                        desconto = @desconto, valorpago = @valorpago 
                        WHERE(id_venda = @id_venda)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_pessoa", venda.IdPessoa);
                cmd.Parameters.AddWithValue("@valor", venda.Valor);
                cmd.Parameters.AddWithValue("@desconto", venda.Desconto);
                cmd.Parameters.AddWithValue("@valorpago", venda.ValorPago);
                cmd.Parameters.AddWithValue("@id_venda", venda.Id);

                retorno = cmd.ExecuteNonQuery();

                if (retorno > 0)
                    resp = "Venda inciada.";
                else
                    resp = "Não foi possível iniciar venda.";

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
