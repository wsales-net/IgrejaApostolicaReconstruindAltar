using DTO;
using System.Data.OleDb;

namespace DAL
{
    public class ItemVendaDAL : Conecta
    {
        public string AddItemVenda(ItemVenda itemVenda)
        {
            string sql, resp;
            int retorno = 0;

            try
            {
                sql = @"INSERT INTO ItemVenda (id_venda, id_produto, quantidade, valor) 
                        VALUES (@id_venda, @id_produto, @quantidade, @valor)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_venda", itemVenda.IdVenda);
                cmd.Parameters.AddWithValue("@id_produto", itemVenda.IdProduto);
                cmd.Parameters.AddWithValue("@quantidade", itemVenda.Quantidade);
                cmd.Parameters.AddWithValue("@valor", itemVenda.Valor);

                retorno = cmd.ExecuteNonQuery();

                if (retorno > 0)
                    resp = "Item(ns) gravado(s) com sucesso.";
                else
                    resp = "Item(ns) não gravado(s).";

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
