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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace teacherCamilaCodeDev
{
    public partial class frmFrase : Form
    {
        public frmFrase()
        {
            InitializeComponent();
            CarregarFrase();
        }

        private void CarregarFrase()
        {
            try
            {
                banco.Conectar();
                string selecionar = "select * from tbl_frase where statusFrase <> 'DESATIVADO' order by nomeFrase;";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvFrase.DataSource = dt;
                dgvFrase.Columns[0].Visible = false;
                dgvFrase.Columns[1].HeaderText = "NOME";
                dgvFrase.Columns[2].HeaderText = "TEXTO";
                dgvFrase.Columns[3].HeaderText = "STATUS";

                dgvFrase.ClearSelection();
                banco.Desconectar();
            }
            catch(Exception erro)
            {
                MessageBox.Show("Erro ao selecionar a frase.\n\n" + erro);
            }
        }

        private void CarregarFraseStatus()
        {
            try
            {
                banco.Conectar();
                string selecionar = "select * from tbl_frase where statusFrase = @status;";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@status", cmbStatus.Text);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvFrase.DataSource = dt;
                dgvFrase.Columns[0].Visible = false;
                dgvFrase.Columns[1].HeaderText = "NOME";
                dgvFrase.Columns[2].HeaderText = "TEXTO";
                dgvFrase.Columns[3].HeaderText = "STATUS";

                dgvFrase.ClearSelection();
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao selecionar o status da frase.\n\n" + erro);
            }
        }

        private void CarregarFraseNome()
        {
            try
            {
                banco.Conectar();
                string selecionar = "select * from tbl_frase where nomeFrase LIKE '%"+txtFrase.Text+"%' order by nomeFrase asc;";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvFrase.DataSource = dt;
                dgvFrase.Columns[0].Visible = false;
                dgvFrase.Columns[1].HeaderText = "NOME";
                dgvFrase.Columns[2].HeaderText = "TEXTO";
                dgvFrase.Columns[3].HeaderText = "STATUS";

                dgvFrase.ClearSelection();
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao selecionar o nome da frase.\n\n" + erro);
            }
        }

        private void DesativarFrase()
        {
            try
            {
                banco.Conectar();
                string alterar = "update tbl_frase set statusFrase = 'DESATIVADO' where idFrase = @codigo;";
                MySqlCommand cmd = new MySqlCommand(alterar, banco.conexao);

                //parametros
                cmd.Parameters.AddWithValue("@codigo", variaveis.codigoFrase);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Frase excluída com sucesso.", "EXCLUIR FRASE");
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao excluir frase.\n\n" + erro);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            new frmMenu().Show();
            Close();
        }

        private void dgvFrase_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAlterar.Enabled = true;
            btnDesativar.Enabled = true;
            variaveis.linhaSelecionada = int.Parse(e.RowIndex.ToString());
            if (variaveis.linhaSelecionada >= 0)
            {
                variaveis.codigoFrase = Convert.ToInt32(dgvFrase[0, variaveis.linhaSelecionada].Value);
            }
        }

        private void dgvFrase_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvFrase.Sort(dgvFrase.Columns[1], ListSortDirection.Ascending);
            dgvFrase.ClearSelection();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStatus.Text == "TODOS")
            {
                CarregarFrase();
            }
            else
            {
                CarregarFraseStatus();
            }
        }

        private void txtFrase_TextChanged(object sender, EventArgs e)
        {
            if (txtFrase.Text == "")
            {
                cmbStatus.Enabled = true;
                cmbStatus.Text = "TODOS";
                CarregarFrase();
            }
            else
            {
                cmbStatus.Enabled = false;
                CarregarFraseNome();
            }
        }

        private void btnDesativar_Click(object sender, EventArgs e)
        {
            if (variaveis.linhaSelecionada >= 0)
            {
                var resposta = MessageBox.Show("Deseja mesmo excluir a frase?", "EXCLUIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resposta == DialogResult.Yes)
                {
                    var resposta2 = MessageBox.Show("Tem certeza? Essa ação não poderá ser desfeita", "CONFIRMAÇÃO", MessageBoxButtons.YesNo);
                    if (resposta2 == DialogResult.Yes)
                    {
                        DesativarFrase();
                        CarregarFrase();
                    }
                }
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (variaveis.linhaSelecionada >= 0)
            {
                variaveis.funcao = "ALTERAR";
                new frmCadFrase().Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Para alterar selecione uma Frase da lista.");
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            variaveis.funcao = "CADASTRAR";
            new frmCadFrase().Show();
            Hide();
        }

        private void frmFrase_Load(object sender, EventArgs e)
        {
            dgvFrase.ClearSelection();
        }
    }
}
