using BLL;
using DTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace View
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            bool validar = UsuarioBLL.CheckLogin(MontarUsuario());

            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                lblMensagem.ForeColor = Color.Red;
                lblMensagem.Text = "Você precisa digitar um nome de usuário";
                txtUsuario.Focus();
            }
            else if (string.IsNullOrEmpty(txtSenha.Text))
            {
                lblMensagem.ForeColor = Color.Red;
                lblMensagem.Text = "Você precisa digitar a senha de usuário";
                txtSenha.Focus();
            }
            else if (validar)
            {
                FrmPrincipal frm = new FrmPrincipal();
                frm.Senha = txtSenha.Text;
                frm.Usuario = txtUsuario.Text;
                this.Visible = false;
                frm.Show();
                MessageBox.Show("Bem vindo ao Sistema, " + txtUsuario.Text, "Menssagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblMensagem.Text = "";
                MessageBox.Show("Usuário ou senha inválido(s)", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private Usuario MontarUsuario()
        {
            Usuario usuario = new Usuario();
            usuario.Login = txtUsuario.Text;
            usuario.Senha = txtSenha.Text;
            return usuario;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtUsuario.Text))
                lblMensagem.Text = "";
            if (e.KeyValue.Equals(13)) //ENTER
            {
                if (String.IsNullOrEmpty(txtUsuario.Text))
                {
                    lblMensagem.ForeColor = Color.Red;
                    lblMensagem.Text = "Você precisa digitar um nome de usuário";
                    txtUsuario.Focus();
                }
                else
                {
                    lblMensagem.Text = "";
                    txtSenha.Focus();
                }
            }
        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSenha.Text))
                lblMensagem.Text = "";
            if (e.KeyValue.Equals(13)) //ENTER
            {
                if (String.IsNullOrEmpty(txtSenha.Text))
                {
                    lblMensagem.ForeColor = Color.Red;
                    lblMensagem.Text = "Você precisa digitar a senha de usuário";
                    txtSenha.Focus();
                }
                else
                {
                    lblMensagem.Text = "";
                    btnEntrar_Click(sender, e);
                }
            }
        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.Equals(27)) //ESC
                Application.Exit();
        }
    }
}
