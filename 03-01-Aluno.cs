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
    public partial class frmAluno : Form
    {
        public frmAluno()
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
                string selecionar = "select * from tbl_aluno where nomeAluno LIKE '%"+txtAluno.Text+"%' order by nomeAluno asc;";
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

        private void DesativarAluno()
        {
            try
            {
                banco.Conectar();
                string alterar = "update tbl_aluno set statusAluno = 'DESATIVADO' where idAluno = @codigo;";
                MySqlCommand cmd = new MySqlCommand(alterar, banco.conexao);

                //parametros
                cmd.Parameters.AddWithValue("@codigo", variaveis.codigoAluno);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Aluno excluído com sucesso.", "EXCLUIR ALUNO");
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao excluir aluno.\n\n" + erro);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            new frmMenu().Show();
            Close();
        }

        private void dgvAluno_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAlterar.Enabled = true;
            btnDesativar.Enabled = true;
            variaveis.linhaSelecionada = int.Parse(e.RowIndex.ToString());
            if (variaveis.linhaSelecionada >= 0)
            {
                variaveis.codigoAluno = Convert.ToInt32(dgvAluno[0, variaveis.linhaSelecionada].Value);
            }
        }

        private void dgvAluno_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvAluno.Sort(dgvAluno.Columns[1], ListSortDirection.Ascending);
            dgvAluno.ClearSelection();
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

        private void txtAluno_TextChanged(object sender, EventArgs e)
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

        private void btnDesativar_Click(object sender, EventArgs e)
        {
            if (variaveis.linhaSelecionada >= 0)
            {
                var resposta = MessageBox.Show("Deseja mesmo excluir o Aluno?", "EXCLUIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resposta == DialogResult.Yes)
                {
                    var resposta2 = MessageBox.Show("Tem certeza? Essa ação não poderá ser desfeita", "CONFIRMAÇÃO", MessageBoxButtons.YesNo);
                    if (resposta2 == DialogResult.Yes)
                    {
                        DesativarAluno();
                        CarregarAluno();
                    }
                }
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (variaveis.linhaSelecionada >= 0)
            {
                variaveis.funcao = "ALTERAR";
                new frmCadAluno().Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Para alterar selecione um aluno da lista.");
            }
        }

        private void frmAluno_Load(object sender, EventArgs e)
        {
            dgvAluno.ClearSelection();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            variaveis.funcao = "CADASTRAR";
            new frmCadAluno().Show();
            Hide();
        }
    }
}
