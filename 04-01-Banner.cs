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
    public partial class frmBanner : Form
    {
        public frmBanner()
        {
            InitializeComponent();
            CarregarBanner();
        }

        private void CarregarBanner()
        {
            try
            {
                banco.Conectar();
                string selecionar = "select * from tbl_banner where statusBanner <> 'DESATIVADO' order by nomeBanner;";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvBanner.DataSource = dt;
                dgvBanner.Columns[0].Visible = false;           //id
                dgvBanner.Columns[1].HeaderText = "Nome";       //Nome
                dgvBanner.Columns[2].HeaderText = "Foto";       //Foto
                dgvBanner.Columns[3].Visible = false;           //alt
                dgvBanner.Columns[4].HeaderText = "Status";     //status

                dgvBanner.ClearSelection();
                banco.Desconectar();
            }
            catch(Exception erro)
            {
                MessageBox.Show("Erro ao selecionar o Banner.\n\n" + erro);
            }
        }

        private void CarregarBannerStatus()
        {
            try
            {
                banco.Conectar();
                string selecionar = "select * from tbl_banner where statusBanner = @status;";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@status", cmbStatus.Text);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvBanner.DataSource = dt;
                dgvBanner.Columns[0].Visible = false;           //id
                dgvBanner.Columns[1].HeaderText = "Nome";       //Nome
                dgvBanner.Columns[2].HeaderText = "Foto";       //Foto
                dgvBanner.Columns[3].Visible = false;           //alt
                dgvBanner.Columns[4].HeaderText = "Status";     //status

                dgvBanner.ClearSelection();
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao selecionar o Banner.\n\n" + erro);
            }
        }

        private void CarregarBannerNome()
        {
            try
            {
                banco.Conectar();//abrir o banco de dados
                string selecionar = "select * from tbl_banner where nomeBanner LIKE '%" + txtBanner.Text + "%' order by nomeBanner asc;";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);//adaptar ao C#
                DataTable dt = new DataTable();//criando uma estrutura de tabela
                da.Fill(dt);//pegar o comando q foi adaptado ao c# e preencher a tabela (dt)

                dgvBanner.DataSource = dt;
                dgvBanner.Columns[0].Visible = false;           //id
                dgvBanner.Columns[1].HeaderText = "Nome";       //Nome
                dgvBanner.Columns[2].HeaderText = "Foto";       //Foto
                dgvBanner.Columns[3].Visible = false;           //alt
                dgvBanner.Columns[4].HeaderText = "Status";     //status

                dgvBanner.ClearSelection();
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao selecionar o Banner.\n\n" + erro);
            }
        }

        private void DesativarBanner()
        {
            try
            {
                banco.Conectar();
                string alterar = "update tbl_banner set statusBanner = 'DESATIVADO' where idBanner = @codigo;";
                MySqlCommand cmd = new MySqlCommand(alterar, banco.conexao);

                //parametros
                cmd.Parameters.AddWithValue("@codigo", variaveis.codigoBanner);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Banner excluído com sucesso.", "EXCLUIR BANNER");
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao excluir banner.\n\n" + erro);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            new frmMenu().Show();
            Close();
        }

        private void dgvBanner_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAlterar.Enabled = true;
            btnDesativar.Enabled = true;
            variaveis.linhaSelecionada = int.Parse(e.RowIndex.ToString());
            if(variaveis.linhaSelecionada >= 0) 
            {
                variaveis.codigoBanner = Convert.ToInt32(dgvBanner[0, variaveis.linhaSelecionada].Value);
            }
        }

        private void dgvBanner_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvBanner.Sort(dgvBanner.Columns[1], ListSortDirection.Ascending);
            dgvBanner.ClearSelection();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStatus.Text == "TODOS")
            {
                CarregarBanner();
            }
            else
            {
                CarregarBannerStatus();
            }
        }

        private void txtBanner_TextChanged(object sender, EventArgs e)
        {
            if (txtBanner.Text == "")
            {
                cmbStatus.Enabled = true;
                cmbStatus.Text = "TODOS";
                CarregarBanner();
            }
            else
            {
                cmbStatus.Enabled = false;
                CarregarBannerNome();
            }
        }

        private void btnDesativar_Click(object sender, EventArgs e)
        {
            if (variaveis.linhaSelecionada >= 0)
            {
                var resposta = MessageBox.Show("Deseja mesmo excluir o banner?", "EXCLUIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resposta == DialogResult.Yes)
                {
                    var resposta2 = MessageBox.Show("Tem certeza? Essa ação não poderá ser desfeita", "CONFIRMAÇÃO", MessageBoxButtons.YesNo);
                    if (resposta2 == DialogResult.Yes)
                    {
                        DesativarBanner();
                        CarregarBanner();
                    }
                }
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if(variaveis.linhaSelecionada >= 0)
            {
                variaveis.funcao = "ALTERAR";
                new frmCadBanner().Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Para alterar selecione um banner da lista.");
            }
        }

        private void frmBanner_Load(object sender, EventArgs e)
        {
            dgvBanner.ClearSelection();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            variaveis.funcao = "CADASTRAR";
            new frmCadBanner().Show();
            Hide();
        }
    }
}
