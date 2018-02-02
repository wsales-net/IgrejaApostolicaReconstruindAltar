using DTO;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace DAL
{
    public class EventoDAL : Conecta
    {
        public static string AddEvento(Evento ev)
        {
            string sql, resp = "";
            int retorno = 0;
            try
            {
                sql = @"INSERT INTO Evento (id_publico, id_endereco, nome, data, hora, descricao, numero, ponto_referencia)
                        VALUES (@id_publico, @id_endereco, @nome, @data, @hora, @descricao, @numero, @ponto_referencia)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_publico", ev.IdPublico);
                cmd.Parameters.AddWithValue("@id_endereco", ev.IdEndereco);
                cmd.Parameters.AddWithValue("@nome", ev.Nome);
                cmd.Parameters.AddWithValue("@data", ev.Data);
                cmd.Parameters.AddWithValue("@hora", ev.Hora);
                cmd.Parameters.AddWithValue("@descricao", ev.Descricao);
                cmd.Parameters.AddWithValue("@numero", ev.Numero);
                cmd.Parameters.AddWithValue("@ponto_referencia", ev.PontoReferencia);

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
                MensagemErroBanco(ex, "AddEvento()");
            }
            return resp;
        }

        public static DataTable FindEventos(string texto)
        {
            DataTable dt = new DataTable();
            string sql;
            try
            {
                sql = @"SELECT id_evento as Código, nome as Nome
                        FROM Evento 
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
                MensagemErroBanco(ex, "FindEventos()");
            }
            return dt;
        }

        public static Evento FindEventoById(int id)
        {
            string sql;
            Evento ev = new Evento();

            try
            {
                sql = "SELECT * FROM Evento WHERE id_evento = @id_evento";

                cmd = GetConexao().CreateCommand();
                cmd.Parameters.AddWithValue("@id_evento", id);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                if (dr.Read()) //Indicado que achou um registro
                {
                    ev.Id = Convert.ToInt32(dr["id_evento"].ToString());
                    ev.IdPublico = Convert.ToInt32(dr["id_publico"].ToString());
                    ev.IdEndereco = Convert.ToInt32(dr["id_endereco"].ToString());
                    ev.Nome = dr["nome"].ToString();
                    ev.Data = Convert.ToDateTime(dr["data"].ToString());
                    ev.Hora = Convert.ToDateTime(dr["hora"].ToString());
                    ev.Descricao = dr["descricao"].ToString();
                    ev.Numero = Convert.ToInt32(dr["numero"].ToString());
                    ev.PontoReferencia = dr["ponto_referencia"].ToString();
                }

                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MensagemErroBanco(ex, "FindEventoById()");
            }
            return ev;
        }

        public static string UpdateEvento(Evento evento)
        {
            string sql, resp = "";
            int retorno;

            try
            {
                sql = @"UPDATE Evento
                        SET  id_publico = @id_publico, id_endereco = @id_endereco,
                        nome = @nome, data = @data, hora = @hora, descricao = @descricao,
                        numero = @numero, ponto_referencia = @ponto_referencia
                        WHERE(id_evento = @id_evento)";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id_publico", evento.IdPublico);
                cmd.Parameters.AddWithValue("@id_endereco", evento.IdEndereco);
                cmd.Parameters.AddWithValue("@nome", evento.Nome);
                cmd.Parameters.AddWithValue("@data", evento.Data);
                cmd.Parameters.AddWithValue("@hora", evento.Hora);
                cmd.Parameters.AddWithValue("@descricao", evento.Descricao);
                cmd.Parameters.AddWithValue("@numero", evento.Numero);
                cmd.Parameters.AddWithValue("@ponto_referencia", evento.PontoReferencia);
                cmd.Parameters.AddWithValue("@id_evento", evento.Id);
                retorno = cmd.ExecuteNonQuery();

                if (retorno > 0)
                    resp = "Evento atualizado com sucesso.";
                else
                    resp = "Evento não atualizado.";

                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MensagemErroBanco(ex, "UpdateEvento()");
            }
            return resp;
        }

        public static bool ValidarEvento(string nome)
        {
            bool resp = false;
            string sql;

            try
            {
                sql = "SELECT * FROM Evento WHERE nome = @nome";

                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@nome", nome);
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                    resp = true;

                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MensagemErroBanco(ex, "ValidarEvento()");
            }
            return resp;
        }
    }
}
