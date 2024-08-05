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
    public partial class frmGame : Form
    {
        public frmGame()
        {
            InitializeComponent();
            CarregarGame();
        }

        private void CarregarGame()
        {
            try
            {
                banco.Conectar();
                string selecionar = "select * from tbl_game where statusGame <> 'DESATIVADO' order by nomeGame;";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvGame.DataSource = dt;
                dgvGame.Columns[0].Visible = false;           //id
                dgvGame.Columns[1].HeaderText = "Nome";       //Nome
                dgvGame.Columns[2].HeaderText = "Link";       //Link
                dgvGame.Columns[3].HeaderText = "Foto";       //Foto
                dgvGame.Columns[4].Visible = false;           //Alt
                dgvGame.Columns[5].HeaderText = "Status";     //Status

                dgvGame.ClearSelection();
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao selecionar o game.\n\n" + erro);
            }
        }

        private void CarregarGameStatus()
        {
            try
            {
                banco.Conectar();
                string selecionar = "select * from tbl_game where statusGame = @status;";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@status", cmbStatus.Text);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvGame.DataSource = dt;
                dgvGame.Columns[0].Visible = false;           //id
                dgvGame.Columns[1].HeaderText = "Nome";       //Nome
                dgvGame.Columns[2].HeaderText = "Link";       //Link
                dgvGame.Columns[3].HeaderText = "Foto";       //Foto
                dgvGame.Columns[4].Visible = false;           //Alt
                dgvGame.Columns[5].HeaderText = "Status";     //Status

                dgvGame.ClearSelection();
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao selecionar o status do game.\n\n" + erro);
            }
        }

        private void CarregarGameNome()
        {
            try
            {
                banco.Conectar();
                string selecionar = "select * from tbl_game where nomeGame LIKE '%" + txtGame.Text + "%' order by nomeGame asc;";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvGame.DataSource = dt;
                dgvGame.Columns[0].Visible = false;           //id
                dgvGame.Columns[1].HeaderText = "Nome";       //Nome
                dgvGame.Columns[2].HeaderText = "Link";       //Link
                dgvGame.Columns[3].HeaderText = "Foto";       //Foto
                dgvGame.Columns[4].Visible = false;           //Alt
                dgvGame.Columns[5].HeaderText = "Status";     //Status

                dgvGame.ClearSelection();
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao selecionar o nome do game.\n\n" + erro);
            }
        }

        private void DesativarGame()
        {
            try
            {
                banco.Conectar();
                string alterar = "update tbl_game set statusGame = 'DESATIVADO' where idGame = @codigo;";
                MySqlCommand cmd = new MySqlCommand(alterar, banco.conexao);

                //parametros
                cmd.Parameters.AddWithValue("@codigo", variaveis.codigoGame);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Game excluído com sucesso.", "EXCLUIR GAME");
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao excluir game.\n\n" + erro);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            new frmMenu().Show();
            Close();
        }

        private void dgvGame_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAlterar.Enabled = true;
            btnDesativar.Enabled = true;
            variaveis.linhaSelecionada = int.Parse(e.RowIndex.ToString());
            if (variaveis.linhaSelecionada >= 0)
            {
                variaveis.codigoGame = Convert.ToInt32(dgvGame[0, variaveis.linhaSelecionada].Value);
            }
        }

        private void dgvGame_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvGame.Sort(dgvGame.Columns[1], ListSortDirection.Ascending);
            dgvGame.ClearSelection();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStatus.Text == "TODOS")
            {
                CarregarGame();
            }
            else
            {
                CarregarGameStatus();
            }
        }

        private void txtGame_TextChanged(object sender, EventArgs e)
        {
            if (txtGame.Text == "")
            {
                cmbStatus.Enabled = true;
                cmbStatus.Text = "TODOS";
                CarregarGame();
            }
            else
            {
                cmbStatus.Enabled = false;
                CarregarGameNome();
            }
        }

        private void btnDesativar_Click(object sender, EventArgs e)
        {
            if (variaveis.linhaSelecionada >= 0)
            {
                var resposta = MessageBox.Show("Deseja mesmo excluir o game?", "EXCLUIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resposta == DialogResult.Yes)
                {
                    var resposta2 = MessageBox.Show("Tem certeza? Essa ação não poderá ser desfeita", "CONFIRMAÇÃO", MessageBoxButtons.YesNo);
                    if (resposta2 == DialogResult.Yes)
                    {
                        DesativarGame();
                        CarregarGame();
                    }
                }
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (variaveis.linhaSelecionada >= 0)
            {
                variaveis.funcao = "ALTERAR";
                new frmCadGame().Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Para alterar selecione um game da lista.");
            }
        }

        private void frmGame_Load(object sender, EventArgs e)
        {
            dgvGame.ClearSelection();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            variaveis.funcao = "CADASTRAR";
            new frmCadGame().Show();
            Hide();
        }
    }
}
