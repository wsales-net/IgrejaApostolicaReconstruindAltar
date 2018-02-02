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
                Data Source=C:/Users/WELL/Documents/GitHub/IgrejaApostolicaReconstruindAltar/View/banco_homologacao.accdb");/*/bin/Debug*/
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

        public static void MensagemErroBanco(Exception ex, string metodo)
        {
            MessageBox.Show("Falha ao consultar - " + metodo + "\nContate o Administrador - (11) 2636-5659\n\n" + ex.ToString(),
                "Erro no Banco de Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
