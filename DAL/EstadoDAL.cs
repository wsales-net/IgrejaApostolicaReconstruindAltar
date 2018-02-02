using DTO;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;

namespace DAL
{
    public class EstadoDAL : Conecta
    {
        public static List<Estado> GetEstado()
        {
            List<Estado> uf = new List<Estado>();
            string sql;
            try
            {
                sql = "SELECT * FROM Estado ORDER BY sigla";
                cmd = GetConexao().CreateCommand();
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Estado estado = new Estado();
                    estado.Id = Convert.ToInt32(dr["id_estado"].ToString());
                    estado.Sigla = dr["sigla"].ToString();
                    uf.Add(estado);
                }

                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MensagemErroBanco(ex, "GetEstado()");
            }
            return uf;
        }

        public static Estado GetEstado(string sigla)
        {
            Estado uf = new Estado();
            string sql;

            try
            {
                sql = "SELECT * FROM ESTADO WHERE sigla = @sigla";
                cmd = GetConexao().CreateCommand();
                cmd.Parameters.AddWithValue("@sigla", sigla);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    uf.Id = Convert.ToInt32(dr["id_estado"].ToString());
                    uf.Sigla = dr["sigla"].ToString();
                }

                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MensagemErroBanco(ex, "GetEstado()");
            }
            return uf;
        }
    }
}
