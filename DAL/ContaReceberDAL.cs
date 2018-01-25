using DTO;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace DAL
{
    public class ContaReceberDAL : Conecta
    {
        public string addConta(ContaReceber cr)
        {
            string sql, resp;
            int retorno;

            try
            {
                sql = @"INSERT INTO ContaReceber (id_venda, id_statuspagamento, data_compra, data_pagamento, data_vencimento) 
                        VALUES (@id_venda, @id_statuspagamento, @data_compra, @data_pagamento, @data_vencimento)";


                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_venda", cr.IdVenda);
                cmd.Parameters.AddWithValue("@id_statuspagamento", cr.IdFormaPagamento);
                cmd.Parameters.AddWithValue("@data_compra", cr.DataCompra);
                cmd.Parameters.AddWithValue("@data_pagamento", cr.DataPagamento == null ? (object)DBNull.Value : cr.DataPagamento);
                cmd.Parameters.AddWithValue("@data_vencimento", cr.DataVencimento);

                /*
                 Para carregar dado de data null:
                  - dado.Data = reader["data"] == DBNull.Value ? (DateTime?) null : Convert.ToDateTime(reader["data"]);
                 */

                retorno = cmd.ExecuteNonQuery();

                if (retorno > 0)
                    resp = "Venda finalizada com sucesso.";
                else
                    resp = "Venda não realizado.";

                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                resp = "ERRO: " + ex.ToString();
            }
            return resp;
        }

        public DataTable FindAllContaReceber()
        {
            DataTable dt = new DataTable();
            string sql;
            try
            {
                sql = @"SELECT Pessoa.nome AS Pessoa, Produto.valor, Produto.nome AS produto, ItemVenda.quantidade, Venda.valorpago, ContaReceber.data_vencimento
                        FROM ((Pessoa INNER JOIN
                        ((Produto INNER JOIN
                        ItemVenda ON Produto.id_produto = ItemVenda.id_produto) INNER JOIN
                        Venda ON ItemVenda.id_venda = Venda.id_venda) ON Pessoa.id_pessoa = Venda.id_pessoa) INNER JOIN
                        ContaReceber ON Venda.id_venda = ContaReceber.id_venda)
                        WHERE  (ContaReceber.data_pagamento IS NULL) AND (ContaReceber.data_vencimento < #" + DateTime.Now + "#)";

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

    }
}
