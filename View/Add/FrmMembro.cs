using BLL;
using DTO;
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace View.Add
{
    public partial class FrmMembro : Form
    {
        private Membro pessoa;

        public FrmMembro()
        {
            InitializeComponent();
        }

        public FrmMembro(string title, Membro pes)
        {
            InitializeComponent();
            this.pessoa = pes;

            if (title == "Alteração de Membros")
            {
                this.Text = "Alteração de Membros";
            }
            else if (title == "Consulta de Membros")
            {
                AlterarCampos();
                btnSalvar.Enabled = false;
            }
        }

        private void AlterarCampos()
        {
            if (cbxStatus.SelectedIndex == 1)
            {
                txtNome.Enabled = false;
                cbxFuncao.Enabled = false;
                cbxEstado.Enabled = false;
                cbxCidade.Enabled = false;
                txtNome.Enabled = false;
                txtEmail.Enabled = false;
                txtNascimento.Enabled = false;
                txtTelefone.Enabled = false;
                txtCelular.Enabled = false;
                txtNumero.Enabled = false;
                txtComplemento.Enabled = false;
                txtCpf.Enabled = false;
                txtRG.Enabled = false;
                txtCep.Enabled = false;
                txtLougradouro.Enabled = false;
                txtBairro.Enabled = false;
                txtNumero.Enabled = false;
                rbtMasculino.Enabled = false;
                rbtFeminino.Enabled = false;
                btnCep.Enabled = false;
                btnFoto.Enabled = false;
            }
            else
            {
                txtNome.Enabled = true;
                cbxFuncao.Enabled = true;
                cbxEstado.Enabled = true;
                cbxCidade.Enabled = true;
                txtNome.Enabled = true;
                txtEmail.Enabled = true;
                txtNascimento.Enabled = true;
                txtTelefone.Enabled = true;
                txtCelular.Enabled = true;
                txtNumero.Enabled = true;
                txtComplemento.Enabled = true;
                txtCpf.Enabled = true;
                txtRG.Enabled = true;
                txtCep.Enabled = true;
                txtLougradouro.Enabled = true;
                txtBairro.Enabled = true;
                txtNumero.Enabled = true;
                rbtMasculino.Enabled = true;
                rbtFeminino.Enabled = true;
                btnCep.Enabled = true;
                btnFoto.Enabled = true;

            }
        }

        private void FrmMembro_Load(object sender, EventArgs e)
        {
            rbtMasculino.Checked = true;

            //LISTA ESTADOS
            cbxEstado.DataSource = EstadoBLL.GetEstado();
            cbxEstado.DisplayMember = "Sigla";
            cbxEstado.ValueMember = "Id";

            //LISTA FUNÇÕES
            ArrayList listaF = FuncaoBLL.GetFuncao();
            for (int i = 0; i < listaF.Count; i++)
            {
                Funcao funcao = (Funcao)listaF[i];
                cbxFuncao.Items.Add(funcao.Id + " - " + funcao.Descricao);
            }

            cbxEstado.SelectedValue = 26;
            cbxCidade.SelectedValue = 9668;

            LimitarCampos();

            String[] elemento = (String[]) cbxFuncao.Items[pessoa.IdFuncao];

            //Quando a tela é carregada 
            CarregarDados(pessoa, elemento[pessoa.IdFuncao], pessoa.Endereco);
        }

        private void LimitarCampos()
        {
            txtNome.MaxLength = 100;
            txtEmail.MaxLength = 100;
            txtLougradouro.MaxLength = 100;
            txtComplemento.MaxLength = 20;
            txtBairro.MaxLength = 30;
        }

        //Carrega dados do banco passado por parametro
        private void CarregarDados(Membro membro, string listFuncao, Endereco endereco)
        {
            if (membro != null)
            {
                //membro.IdFuncao;
                cbxEstado.SelectedValue = endereco.Cidade.Estado.Id;
                cbxCidade.SelectedValue = endereco.Cidade.Id;
                txtNome.Text = membro.Nome;
                txtEmail.Text = membro.Email;
                txtNascimento.Text = membro.DataNascimento.ToString();
                txtTelefone.Text = membro.Telefone;
                txtCelular.Text = membro.Celular;
                txtNumero.Text = membro.Numero.ToString();
                txtComplemento.Text = membro.Complemento;
                txtCpf.Text = membro.Cpf;
                txtRG.Text = membro.Rg;
                ptbMembro.ImageLocation = Application.StartupPath.ToString() + "\\fotos\\" + membro.Nome + ".png";
                txtCep.Text = endereco.Cep;
                txtLougradouro.Text = endereco.Logradouro;
                txtBairro.Text = endereco.Bairro;
                txtNumero.Text = membro.Numero.ToString();

                if (membro.Status == true)
                    cbxStatus.SelectedIndex = 0;
                else
                    cbxStatus.SelectedIndex = 1;

                if (membro.Sexo == "Masculino")
                    rbtMasculino.Checked = true;
                else
                    rbtFeminino.Checked = true;

                btnSalvar.Text = "Salvar";
                btnCancelar.Text = "Fechar";
            }
            else
            {
                cbxFuncao.Text = "Escolha a função";
                cbxStatus.SelectedIndex = 0;
                txtNascimento.Text = DateTime.Now.ToShortDateString();
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                lblMensagem.ForeColor = Color.DarkGreen;
                if (this.Text == "Alteração de Membros" && btnSalvar.Text == "Salvar")
                {
                    lblMensagem.Text = MembroBLL.UpdateMembro(MontarMembro());
                    if (lblMensagem.Text.Equals("Membro não atualizado."))
                        lblMensagem.ForeColor = Color.Red;
                }
                else if (new MembroBLL().ValidarMembro(txtNome.Text))
                {
                    lblMensagem.ForeColor = Color.Orange;
                    lblMensagem.Text = "Membro já possui cadastro no sistema.";
                }
                else
                {
                    ptbMembro.Image.Save(Application.StartupPath.ToString() + "\\fotos\\" + txtNome.Text + ".png", System.Drawing.Imaging.ImageFormat.Png);
                    lblMensagem.Text = new MembroBLL().AddMembro(MontarMembro());
                    if (lblMensagem.Text.Equals("Cadastro não realizado."))
                        lblMensagem.ForeColor = Color.Red;
                    else
                        LimparCampos();
                }
            }
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            txtCpf.Text = "";
            txtRG.Text = "";
            txtEmail.Text = "";
            txtNascimento.Text = "";
            txtTelefone.Text = "";
            txtCelular.Text = "9";
            cbxFuncao.Text = "Escolha a Função";
            txtCep.Text = "";
            txtLougradouro.Text = "";
            txtNumero.Value = 0;
            txtComplemento.Text = "";
            txtBairro.Text = "";
            ptbMembro.ImageLocation = Application.StartupPath.ToString() + "../../../Resources/user.png";
            ptbMembro.Show();
            txtNome.Focus();
        }

        private Membro MontarMembro()
        {
            Membro membro = new Membro();
            string[] s = cbxFuncao.Text.Split('-');

            #region Salvar Foto
            string antigocaminho = "";
            if (pessoa != null)
            {
                membro.Id = pessoa.Id;
                antigocaminho = Application.StartupPath.ToString() + "\\fotos\\" + this.pessoa.Nome + ".png";
                string novocaminho = Application.StartupPath.ToString() + "\\fotos\\" + txtNome.Text + ".png";
                if (File.Exists(novocaminho))
                    File.Delete(novocaminho);
                File.Move(antigocaminho, novocaminho);
                membro.Foto = Application.StartupPath.ToString() + "\\fotos\\" + txtNome.Text + ".png";
            }
            else
                membro.Foto = Application.StartupPath.ToString() + "\\fotos\\" + txtNome.Text + ".png";
            #endregion

            membro.IdFuncao = int.Parse(s[0]);
            //membro.IdEndereco = new EnderecoDAO().getEndereco(txtCep.Text);
            membro.Nome = txtNome.Text;

            //Verificar o sexo selecionado
            if (rbtMasculino.Checked)
                membro.Sexo = rbtMasculino.Text;
            else
                membro.Sexo = rbtFeminino.Text;

            membro.DataNascimento = DateTime.Parse(txtNascimento.Text);
            membro.Telefone = txtTelefone.Text;
            membro.Celular = txtCelular.Text;
            membro.Numero = Convert.ToInt32(txtNumero.Text);
            membro.Complemento = txtComplemento.Text;

            if (string.IsNullOrEmpty(txtEmail.Text))
                membro.Email = null;
            else
                membro.Email = txtEmail.Text;

            if (!Regex.Match(txtCpf.Text.Replace(",", "."), @"^\d{3}\.\d{3}\.\d{3}-\d{2}$").Success)
                membro.Cpf = null;
            else
                membro.Cpf = txtCpf.Text;

            if (!Regex.Match(txtRG.Text.Replace(",", "."), @"[0-9]{1}?\.[0-9]{3}?\.[0-9]{3}?").Success)
                membro.Rg = null;
            else
                membro.Rg = txtRG.Text;

            membro.DataRegistro = DateTime.Parse(DateTime.Now.ToString());
            if (cbxStatus.SelectedIndex == 0)
                membro.Status = true;
            else
                membro.Status = false;

            return membro;
        }

        private bool ValidarCampos()
        {
            DateTime data = DateTime.Parse(txtNascimento.Text);

            if (String.IsNullOrEmpty(txtNome.Text))
            {
                lblMensagem.Text = "Digitar o nome.";
                lblMensagem.ForeColor = Color.Red;
                txtNome.Focus();
                return false;
            }
            if (DateTime.Compare(data, DateTime.Now) > 0 || data == DateTime.Today)
            {
                lblMensagem.Text = "Data de nascimento não pode ser maior ou igual a data atual.";
                lblMensagem.ForeColor = Color.Red;
                txtNascimento.Focus();
                return false;
            }
            if (!Regex.Match(txtTelefone.Text, @"^\(?\d{2}\)?[\s-]?[\s9]?\d{4}-?\d{4}$").Success)
            {
                lblMensagem.Text = "Digite um número de telefone correto.";
                lblMensagem.ForeColor = Color.Red;
                txtTelefone.Focus();
                return false;
            }
            if (!Regex.Match(txtCelular.Text, @"^\(?\d{2}\)?[\s-]?[\s9]?\d{4}-?\d{4}$").Success)
            {
                lblMensagem.Text = "Digite um número de celular correto.";
                lblMensagem.ForeColor = Color.Red;
                txtCelular.Focus();
                return false;
            }
            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                if (!Regex.Match(txtEmail.Text, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$").Success)
                {
                    lblMensagem.Text = "Digite um e-mail correto.";
                    lblMensagem.ForeColor = Color.Red;
                    txtCelular.Focus();
                    return false;
                }
            }
            if (cbxFuncao.Text == "Escolha a Função")
            {
                lblMensagem.Text = "Selecione uma função para o membro.";
                lblMensagem.ForeColor = Color.Red;
                cbxFuncao.Focus();
                cbxFuncao.SelectAll();
                return false;
            }
            if (!Regex.Match(txtCep.Text, @"^[0-9]{5}-[0-9]{3}$").Success)
            {
                lblMensagem.Text = "Digite um número de CEP correto.";
                lblMensagem.ForeColor = Color.Red;
                txtCep.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtLougradouro.Text))
            {
                lblMensagem.Text = "Digite o logradouro";
                lblMensagem.ForeColor = Color.Red;
                txtLougradouro.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtNumero.Text))
            {
                lblMensagem.Text = "Digite o número.";
                lblMensagem.ForeColor = Color.Red;
                txtNumero.Focus();
                return false;
            }
            if (String.IsNullOrEmpty(txtBairro.Text))
            {
                lblMensagem.Text = "Digite o bairro";
                lblMensagem.ForeColor = Color.Red;
                txtBairro.Focus();
                return false;
            }
            return true;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCep_Click(object sender, EventArgs e)
        {

        }
    }
}
