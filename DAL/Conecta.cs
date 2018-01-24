using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace DAL
{
    public class Conecta
    {
        protected static OleDbCommand cmd;
        protected static OleDbDataReader dr;

        public static OleDbConnection GetConexao()
        {
            OleDbConnection conexao;
            try
            {
                conexao = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;
                Data Source=C:/Users/suporte 24/Downloads/IgrejaApostolicaReconstruindoAltar/IARAltar/bancodados.accdb");/*/bin/Debug*/
                conexao.Open();
                return conexao;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha ao abrir banco de dados\n" + ex.ToString(), "ERRO",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }        
    }
}
