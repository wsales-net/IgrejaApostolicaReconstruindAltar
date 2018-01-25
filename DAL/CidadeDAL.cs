using DTO;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;

namespace DAL
{
    public class CidadeDAL : Conecta
    {
        public static List<Cidade> GetCidades(string id_estado)
        {
            List<Cidade> lista = new List<Cidade>();
            string sql;

            try
            {
                sql = @"SELECT * FROM CIDADE C
                        INNER JOIN ESTADO U ON C.ID_ESTADO = U.ID_ESTADO
                        WHERE C.ID_ESTADO = @id_estado";

                //SqlConnection
                cmd = GetConexao().CreateCommand();
                cmd.Parameters.AddWithValue("@id_estado", id_estado);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Cidade cid = new Cidade();
                    //cid.Id = dr.GetInt32(1);
                    cid.Id = int.Parse(dr["id_cidade"].ToString());
                    //cid.Descricao = dr["descricao"].ToString(); //NÃO FUNCIONA, NÃO SEI PQ
                    cid.Descricao = dr.GetString(2);
                    lista.Add(cid);
                }
                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Falha ao consultar\n" + ex.ToString(), "ERRO \nContato o Administrador!" +
                    "(11) 2636-5659", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lista;
        }

        public static Cidade GetCidade(string id_cidade)
        {
            Cidade cid = new Cidade();
            string sql;
            try
            {
                sql = "SELECT * FROM CIDADE WHERE id_cidade = @id_cidade";

                //SqlConnection
                cmd = GetConexao().CreateCommand();
                cmd.Parameters.AddWithValue("@id_cidade", id_cidade);
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    cid.Id = int.Parse(dr["id_cidade"].ToString());
                    cid.Descricao = dr["descricao"].ToString();
                }
                dr.Dispose();
                cmd.Dispose();
                GetConexao().Dispose();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Falha ao consultar\n" + ex.ToString(), "ERRO \nContato o Administrador!" +
                    "(11) 2636-5659", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return cid;
        }
    }
}
