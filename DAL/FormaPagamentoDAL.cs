using DTO;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;

namespace DAL
{
    public class FormaPagamentoDAL : Conecta
    {
        public FormaPagamento FindFormaPagById(string id)
        {
            string sql;
            FormaPagamento formaPagamento = new FormaPagamento();

            try
            {
                sql = "SELECT * FROM StatusPagamento WHERE id_statuspagamento = @id_statuspagamento";

                cmd = GetConexao().CreateCommand();
                cmd.Parameters.AddWithValue("@id_statuspagamento", id);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                if (dr.Read()) //Indicado que achou um registro
                {
                    formaPagamento.Id = int.Parse(dr["id_statuspagamento"].ToString());
                    formaPagamento.Descricao = dr["descricao"].ToString();
                }
                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MensagemErroBanco(ex, "FindFormaPagById()");
            }
            return formaPagamento;
        }

        public List<FormaPagamento> ListarFormaPagamento()
        {
            List<FormaPagamento> lista = new List<FormaPagamento>();
            string sql;

            try
            {
                sql = "SELECT * FROM StatusPagamento";
                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    FormaPagamento formaPagamento = new FormaPagamento();
                    formaPagamento.Id = int.Parse(dr["id_statuspagamento"].ToString());
                    formaPagamento.Descricao = dr["descricao"].ToString();
                    lista.Add(formaPagamento);
                }

                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MensagemErroBanco(ex, "ListarFormaPagamento()");
            }
            return lista;
        }
    }
}
