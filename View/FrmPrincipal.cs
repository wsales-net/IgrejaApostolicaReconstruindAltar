using BLL;
using System;
using System.Windows.Forms;

namespace View
{
    public partial class FrmPrincipal : Form
    {
        public string Senha { get; set; }
        public string Usuario { get; set; }

        public FrmPrincipal()
        {
            InitializeComponent();

            var resp = ContaReceberBLL.FindAllContaReceber();
            if (resp != null)
                lblLink.Text = "Há venda(s) de produto(s) não pago(s).";

            string texto = "Há produtos no sistema que passaram da data de vencimento e não foi efetuado pagamento. \nPara verificar ou confirmar a compra desses produtos clique no link abaixo.";
            toolTip.SetToolTip(lblLink, texto);

            object sender = null;
            EventArgs e = null;
            //efetuarPagamentos(sender, e);
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }
    }
}
