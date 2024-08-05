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
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
            CarregarAluno();
        }

        private void CarregarAluno()
        {
            try
            {
                banco.Conectar();//abrir o banco de dados
                string selecionar = "select * from tbl_aluno where statusAluno <> 'DESATIVADO' order by nomeAluno;";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);//adaptar ao C#
                DataTable dt = new DataTable();//criando uma estrutura de tabela
                da.Fill(dt);//pegar o comando q foi adaptado ao c# e preencher a tabela (dt)

                dgvAluno.DataSource = dt;//coloca a tabela no DataGridView
                dgvAluno.Columns[0].Visible = false;
                dgvAluno.Columns[1].HeaderText = "NOME";
                dgvAluno.Columns[2].HeaderText = "TELEFONE";
                dgvAluno.Columns[3].HeaderText = "E-MAIL";
                dgvAluno.Columns[4].Visible = false;
                dgvAluno.Columns[5].HeaderText = "FOTO";
                dgvAluno.Columns[6].Visible = false;
                dgvAluno.Columns[7].HeaderText = "STATUS";
                dgvAluno.Columns[8].Visible = false;

                dgvAluno.ClearSelection();//Não ficar nada selecionado
                banco.Desconectar();//fechar o banco
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao selecionar o Aluno.\n\n" + erro);
            }
        }

        private void CarregarAlunoStatus()
        {
            try
            {
                banco.Conectar();//abrir o banco de dados
                string selecionar = "select * from tbl_aluno where statusAluno = @status;";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@status", cmbStatus.Text);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);//adaptar ao C#
                DataTable dt = new DataTable();//criando uma estrutura de tabela
                da.Fill(dt);//pegar o comando q foi adaptado ao c# e preencher a tabela (dt)

                dgvAluno.DataSource = dt;//coloca a tabela no DataGridView
                dgvAluno.Columns[0].Visible = false;
                dgvAluno.Columns[1].HeaderText = "NOME";
                dgvAluno.Columns[2].HeaderText = "TELEFONE";
                dgvAluno.Columns[3].HeaderText = "E-MAIL";
                dgvAluno.Columns[4].Visible = false;
                dgvAluno.Columns[5].HeaderText = "FOTO";
                dgvAluno.Columns[6].Visible = false;
                dgvAluno.Columns[7].HeaderText = "STATUS";
                dgvAluno.Columns[8].Visible = false;

                dgvAluno.ClearSelection();//Não ficar nada selecionado
                banco.Desconectar();//fechar o banco
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao selecionar o status do aluno.\n\n" + erro);
            }
        }

        private void CarregarAlunoNome()
        {
            try
            {
                banco.Conectar();//abrir o banco de dados
                string selecionar = "select * from tbl_aluno where nomeAluno LIKE '%" + txtAluno.Text + "%' order by nomeAluno asc;";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);//adaptar ao C#
                DataTable dt = new DataTable();//criando uma estrutura de tabela
                da.Fill(dt);//pegar o comando q foi adaptado ao c# e preencher a tabela (dt)

                dgvAluno.DataSource = dt;//coloca a tabela no DataGridView
                dgvAluno.Columns[0].Visible = false;
                dgvAluno.Columns[1].HeaderText = "NOME";
                dgvAluno.Columns[2].HeaderText = "TELEFONE";
                dgvAluno.Columns[3].HeaderText = "E-MAIL";
                dgvAluno.Columns[4].Visible = false;
                dgvAluno.Columns[5].HeaderText = "FOTO";
                dgvAluno.Columns[6].Visible = false;
                dgvAluno.Columns[7].HeaderText = "STATUS";
                dgvAluno.Columns[8].Visible = false;

                dgvAluno.ClearSelection();//Não ficar nada selecionado
                banco.Desconectar();//fechar o banco
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao selecionar o nome do aluno.\n\n" + erro);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            Close();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            timer1.Start();
            lblUsuario.Text = variaveis.usuario;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblData.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void btnAluno_Click(object sender, EventArgs e)
        {
            new frmAluno().Show();
            Hide();
        }

        private void btnBanner_Click(object sender, EventArgs e)
        {
            new frmBanner().Show();
            Hide();
        }

        private void btnFrase_Click(object sender, EventArgs e)
        {
            new frmFrase().Show();
            Hide();
        }

        private void btnGame_Click(object sender, EventArgs e)
        {
            new frmGame().Show();
            Hide();
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            if (txtAluno.Text == "")
            {
                cmbStatus.Enabled = true;
                cmbStatus.Text = "TODOS";
                CarregarAluno();
            }
            else
            {
                cmbStatus.Enabled = false;
                CarregarAlunoNome();
            }
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStatus.Text == "TODOS")
            {
                CarregarAluno();
            }
            else
            {
                CarregarAlunoStatus();
            }
        }
    }
}
