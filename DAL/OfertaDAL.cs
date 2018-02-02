using DTO;
using System.Data.OleDb;

namespace DAL
{
    public class OfertaDAL : Conecta
    {
        public string AddOferta(Oferta of)
        {
            string sql, resp = "";
            int retorno;

            try
            {
                sql = @"INSERT INTO Oferta (valor, dataentrada) 
                        VALUES (@valor, @dataentrada)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@valor", of.Valor);
                cmd.Parameters.AddWithValue("@dataentrada", of.DataEntrada);

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
                MensagemErroBanco(ex, "GetProdutos()");
            }
            return resp;
        }
    }
}
