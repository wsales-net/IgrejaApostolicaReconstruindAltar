using DTO;
using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace DAL
{
    public class ContaPagarDAL : Conecta
    {
        public static string AddConta(ContaPagar cp)
        {
            string sql, resp = "";
            int retorno;

            try
            {
                sql = @"INSERT INTO ContasPagar (id_tipocontapagar, descricao, valor, data_vencimento, data_pagamento, status) 
                        VALUES (@id_tipocontapagar, @descricao, @valor, 
                        @data_vencimento, @data_pagamento, @status)";

                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_tipocontapagar", cp.IdTipoContaPagar);
                cmd.Parameters.AddWithValue("@descricao", cp.Descricao);
                cmd.Parameters.AddWithValue("@valor", cp.Valor);
                cmd.Parameters.AddWithValue("@data_vencimento", cp.DataVencimento);
                cmd.Parameters.AddWithValue("@data_pagamento", cp.DataPagamento);
                cmd.Parameters.AddWithValue("@status", cp.Status);

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
                MensagemErroBanco(ex, "AddConta()");
            }
            return resp;
        }

        public static ContaPagar FindContaById(int id)
        {
            string sql;
            ContaPagar cp = new ContaPagar();

            try
            {
                
                sql = "SELECT * FROM ContasPagar WHERE id_contaspagar = @id_contaspagar";

                cmd = GetConexao().CreateCommand();
                cmd.Parameters.AddWithValue("@id_contaspagar", id);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                if (dr.Read()) //Indicado que achou um registro
                {
                    cp.Id = Convert.ToInt32(dr["id_contaspagar"].ToString());
                    cp.IdTipoContaPagar = Convert.ToInt32(dr["id_tipocontapagar"].ToString());
                    cp.Descricao = dr["Descricao"].ToString();
                    cp.Valor = Convert.ToDouble(dr["Valor"].ToString());
                    cp.DataVencimento = Convert.ToDateTime(dr["data_vencimento"].ToString());
                    cp.DataPagamento = Convert.ToDateTime(dr["data_pagamento"].ToString());
                    cp.Status = Convert.ToBoolean(dr["status"].ToString());
                }
                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MensagemErroBanco(ex, "FindContaById()");
            }
            return cp;
        }

        public static bool VerificarContas(string data)
        {
            bool resp = false;
            string sql;
            try
            {
                
                sql = @"SELECT id_contaspagar, id_tipocontapagar, descricao, valor, data_vencimento, data_pagamento, status
                        FROM ContasPagar
                        WHERE (data_vencimento < #" + data + "#) AND (status = False)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@data", data);
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
                MensagemErroBanco(ex, "VerificarContas()");
            }
            return resp;
        }
    }
}
