using DTO;
using System.Data.OleDb;

namespace DAL
{
    public class DizimoDAL : Conecta
    {
        public static string AddDizimo(Dizimo dizimo)
        {
            string resp, sql;
            int retorno;
            try
            {
                sql = @"INSERT INTO Dizimo (id_pessoa, valor, dataentrada, observacao) 
                        VALUES (@id_pessoa, @valor, @dataentrada, @observacao)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_pessoa", dizimo.IdMembro);
                cmd.Parameters.AddWithValue("@valor", dizimo.Valor);
                cmd.Parameters.AddWithValue("@dataentrada", dizimo.DataEntrada);
                cmd.Parameters.AddWithValue("@observacao", dizimo.Observacao);

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
    }
}
