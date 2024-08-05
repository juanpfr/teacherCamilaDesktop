using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace teacherCamilaCodeDev
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void Login()
        {
            try
            {
                banco.Conectar();
                string selecionar = "select usuario, senha from tbl_usuario where usuario = @usuario and senha = @senha and status = @status;";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@usuario", variaveis.usuario);
                cmd.Parameters.AddWithValue("@senha", variaveis.senha);
                cmd.Parameters.AddWithValue("@status", "ATIVO");
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    variaveis.usuario = reader.GetString(0);
                    new frmMenu().Show();
                    Hide();
                }
                else
                {
                    variaveis.tentativa = variaveis.tentativa + 1;
                    if (variaveis.tentativa == 3)
                    {
                        Application.Exit();
                    }
                    else
                    {
                        MessageBox.Show("ACESSO NEGADO\n\nVocê tem mais " + (3 - variaveis.tentativa) + "tentativas.");
                        txtUsuario.Clear();
                        txtSenha.Clear();
                        txtUsuario.Focus();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Erro ao efetuar login");
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnEntrar_MouseEnter(object sender, EventArgs e)
        {
            btnEntrar.BackgroundImage = Properties.Resources.btnEntrarCinza;
            btnEntrar.ForeColor = Color.White;
        }

        private void btnEntrar_MouseLeave(object sender, EventArgs e)
        {
            btnEntrar.BackgroundImage = Properties.Resources.btnEntrar;
            btnEntrar.ForeColor= Color.FromArgb(208, 0, 111);
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            variaveis.usuario = txtUsuario.Text;
            variaveis.senha = txtSenha.Text;

            if (variaveis.usuario == "" && variaveis.senha == "")
            {
                new frmMenu().Show();
                Hide();
            }
            else
            {
                Login();
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtSenha.Enabled = true;
                txtSenha.Focus();
            }
        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnEntrar.Enabled = true;
                btnEntrar.Focus();
            }
        }
    }
}
