using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace teacherCamilaCodeDev
{
    public partial class frmCadFrase : Form
    {
        public frmCadFrase()
        {
            InitializeComponent();
        }

        private void InserirFrase()
        {
            try
            {
                banco.Conectar();
                string inserir = "insert into tbl_frase(nomeFrase, textoFrase, statusFrase) values (@nome, @texto, @status);";
                MySqlCommand cmd = new MySqlCommand(inserir, banco.conexao);

                //parameters
                cmd.Parameters.AddWithValue("@nome", variaveis.nomeFrase);
                cmd.Parameters.AddWithValue("@texto", variaveis.textoFrase);
                cmd.Parameters.AddWithValue("@status", variaveis.statusFrase);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Frase cadastrada com sucesso.", "CADASTRO DE FRASE");
                banco.Desconectar();
            }
            catch(Exception erro)
            {
                MessageBox.Show("Erro ao cadastrar frase. \n\n" + erro);
            }
        }

        private void CarregarDadosFrase()
        {
            try
            {
                banco.Conectar();
                string selecionar = "select * from tbl_frase where idFrase = @codigo;";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@codigo", variaveis.codigoFrase);
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    variaveis.nomeFrase = dr.GetString(1);
                    variaveis.textoFrase = dr.GetString(2);
                    variaveis.statusFrase = dr.GetString(3);

                    txtNome.Text = variaveis.nomeFrase;
                    txtFrase.Text = variaveis.textoFrase;
                    cmbStatus.Text = variaveis.statusFrase;
                }
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar dados da frase.\n\n" + erro);
            }
        }

        private void AlterarFrase()
        {
            try
            {
                banco.Conectar();
                string inserir = "update tbl_frase set nomeFrase = @nome, textoFrase = @texto, statusFrase = @status where idFrase = @codigo;";
                MySqlCommand cmd = new MySqlCommand(inserir, banco.conexao);

                //parameters
                cmd.Parameters.AddWithValue("@nome", variaveis.nomeFrase);
                cmd.Parameters.AddWithValue("@texto", variaveis.textoFrase);
                cmd.Parameters.AddWithValue("@status", variaveis.statusFrase);
                cmd.Parameters.AddWithValue("@codigo", variaveis.codigoFrase);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Frase alterada com sucesso.", "ALTERAÇÃO DE FRASE");
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao alterar frase. \n\n" + erro);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            new frmFrase().Show();
            Close();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Clear();
            txtFrase.Clear();
            cmbStatus.SelectedIndex = -1;
        }

        private void frmCadFrase_Load(object sender, EventArgs e)
        {
            if (variaveis.funcao == "CADASTRAR")
            {
                lblCadFrase.Text = "Cadastro de Frase";
            }
            else if (variaveis.funcao == "ALTERAR")
            {
                lblCadFrase.Text = "Alteração de Frase";
                CarregarDadosFrase();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            lblNome.ForeColor = Color.White;
            lblFrase.ForeColor = Color.White;
            lblStatus.ForeColor = Color.White;

            if (txtNome.Text == "")
            {
                lblNome.ForeColor = Color.Red;
                txtNome.Focus();
            }
            if (txtFrase.Text == "")
            {
                lblFrase.ForeColor = Color.Red;
                txtFrase.Focus();
            }
            if (cmbStatus.Text == "")
            {
                lblStatus.ForeColor = Color.Red;
                cmbStatus.Focus();
            }
            else
            {
                variaveis.nomeFrase = txtNome.Text;
                variaveis.textoFrase = txtFrase.Text;
                variaveis.statusFrase = cmbStatus.Text;

                if (variaveis.funcao == "CADASTRAR")
                {
                    InserirFrase();
                    btnVoltar.PerformClick();
                }
                else if (variaveis.funcao == "ALTERAR")
                {
                    AlterarFrase();
                }
            }
        }
    }
}
