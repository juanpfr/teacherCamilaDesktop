using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace teacherCamilaCodeDev
{
    public partial class frmCadAluno : Form
    {
        public frmCadAluno()
        {
            InitializeComponent();
        }

        //MÉTODOS PARA FOTOS FTP

        /*VALIDAÇÃO FTP*/

        private bool ValidarFTP()
        {
            if (string.IsNullOrEmpty(variaveis.enderecoServidorFtp) || string.IsNullOrEmpty(variaveis.usuarioFtp) || string.IsNullOrEmpty(variaveis.senhaFtp))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /*CONVERTER A IMAGEM EM BYTE*/

        public byte[] GetImgToByte(string caminhoArquivoFtp)
        {
            WebClient ftpAluno = new WebClient();
            ftpAluno.Credentials = new NetworkCredential(variaveis.usuarioFtp, variaveis.senhaFtp);
            try
            {

                byte[] imageToByte = ftpAluno.DownloadData(caminhoArquivoFtp);
                return imageToByte;
            }
            catch
            {

                byte[] imageToByte = ftpAluno.DownloadData("ftp://u283879542.teachercamila@smpsistema.com.br/admin/img/aluno/sem-foto.jpg");
                return imageToByte;
            }
        }

        /*CONVERTER A IMAGEM DE BYTE PARA IMAGEM*/

        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        //Métodos

        private void InserirAluno()
        {
            try
            {
                banco.Conectar();
                string inserir = "insert into tbl_aluno(nomeAluno, telefoneAluno, emailAluno, senhaAluno, fotoAluno, altAluno, statusAluno) values(@nome, @telefone, @email, @senha, @foto, @alt, @status);";
                MySqlCommand cmd = new MySqlCommand(inserir, banco.conexao);

                //parametros
                cmd.Parameters.AddWithValue("@nome", variaveis.nomeAluno);
                cmd.Parameters.AddWithValue("@telefone", variaveis.telefoneAluno);
                cmd.Parameters.AddWithValue("@email", variaveis.emailAluno);
                cmd.Parameters.AddWithValue("@senha", variaveis.senhaAluno);
                cmd.Parameters.AddWithValue("@foto", variaveis.fotoAluno);
                cmd.Parameters.AddWithValue("@alt", variaveis.altAluno);
                cmd.Parameters.AddWithValue("@status", variaveis.statusAluno);
                //fim parametros

                cmd.ExecuteNonQuery();
                MessageBox.Show("Aluno cadastrado com sucesso.", "CADASTRO DE ALUNO");
                banco.Desconectar();

                if (ValidarFTP())
                {
                    if (!string.IsNullOrEmpty(variaveis.fotoAluno))
                    {
                        string urlEnviarArquivo = variaveis.enderecoServidorFtp + "img/aluno/" + Path.GetFileName(variaveis.fotoAluno);
                        try
                        {
                            ftp.EnviarArquivoFtp(variaveis.caminhoFotoAluno, urlEnviarArquivo, variaveis.usuarioFtp, variaveis.senhaFtp);
                        }
                        catch
                        {
                            MessageBox.Show("Foto não foi selecionada ou existente no servidor.", "FOTO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao inserir Aluno.\n\n" + erro);
            }
        }

        private void CarregarDadosAluno()
        {
            try
            {
                banco.Conectar();
                string selecionar = "select * from tbl_aluno where idAluno = @codigo;";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@codigo", variaveis.codigoAluno);
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    variaveis.nomeAluno = dr.GetString(1);
                    variaveis.telefoneAluno = dr.GetString(2);
                    variaveis.emailAluno = dr.GetString(3);
                    variaveis.senhaAluno = dr.GetString(4);
                    // Armazenar o caminho completo com o prefixo para uso interno
                    string caminhoFotoAluno = dr.GetString(5);
                    variaveis.fotoAluno = caminhoFotoAluno;

                    // Remover prefixo "aluno/" apenas para exibir a imagem
                    string caminhoParaExibir = caminhoFotoAluno.Replace("aluno/", "");

                    variaveis.altAluno = dr.GetString(6);
                    variaveis.statusAluno = dr.GetString(7);

                    txtNome.Text = variaveis.nomeAluno;
                    txtTelefone.Text = variaveis.telefoneAluno;
                    txtEmail.Text = variaveis.emailAluno;
                    txtSenha.Text = variaveis.senhaAluno;
                    pctAluno.Image = ByteToImage(GetImgToByte(variaveis.enderecoServidorFtp + "img/aluno/" + caminhoParaExibir));
                    cmbStatus.Text = variaveis.statusAluno;
                }
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar dados do aluno.\n\n" + erro);
            }
        }

        private void AlterarAluno()
        {
            try
            {
                banco.Conectar();
                string inserir = "update tbl_aluno set nomeAluno = @nome, telefoneAluno = @telefone, emailAluno = @email, senhaAluno = @senha, fotoAluno = @foto, altAluno = @alt, statusAluno = @status where idAluno = @codigo;";
                MySqlCommand cmd = new MySqlCommand(inserir, banco.conexao);

                //parametros
                cmd.Parameters.AddWithValue("@nome", variaveis.nomeAluno);
                cmd.Parameters.AddWithValue("@telefone", variaveis.telefoneAluno);
                cmd.Parameters.AddWithValue("@email", variaveis.emailAluno);
                cmd.Parameters.AddWithValue("@senha", variaveis.senhaAluno);

                // Verifique se a foto foi alterada antes de atualizar
                if (variaveis.atFotoAluno == "S")
                {
                    cmd.Parameters.AddWithValue("@foto", variaveis.fotoAluno);
                }
                else
                {
                    // Não altere o campo fotoAluno se não houver nova foto
                    string fotoAtual = ObterFotoAtual(variaveis.codigoAluno);
                    cmd.Parameters.AddWithValue("@foto", fotoAtual);
                }

                cmd.Parameters.AddWithValue("@alt", variaveis.altAluno);
                cmd.Parameters.AddWithValue("@status", variaveis.statusAluno);
                cmd.Parameters.AddWithValue("@codigo", variaveis.codigoAluno);
                //fim parametros

                cmd.ExecuteNonQuery();
                MessageBox.Show("Aluno alterado com sucesso.", "ALTERAÇÃO ALUNO");
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao alterar dados do Aluno.\n\n" + erro);
            }
        }

        private string ObterFotoAtual(int idAluno)
        {
            try
            {
                banco.Conectar();
                string selecionar = "select fotoAluno from tbl_aluno where idAluno = @codigo;";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@codigo", idAluno);
                object resultado = cmd.ExecuteScalar();
                banco.Desconectar();

                return resultado != DBNull.Value ? resultado.ToString() : "";
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao obter a foto atual do aluno.\n\n" + erro);
                return "";
            }
        }


        private void AlterarFotoAluno()
        {
            try
            {
                banco.Conectar();
                string alterar = "update tbl_aluno set fotoAluno = @foto where idAluno = @codigo;";
                MySqlCommand cmd = new MySqlCommand(alterar, banco.conexao);

                //parametros
                cmd.Parameters.AddWithValue("@foto", variaveis.fotoAluno);
                cmd.Parameters.AddWithValue("@codigo", variaveis.codigoAluno);
                //fim parametros

                cmd.ExecuteNonQuery();
                banco.Desconectar();

                if (ValidarFTP())
                {
                    if (!string.IsNullOrEmpty(variaveis.fotoAluno))
                    {
                        string urlEnviarArquivo = variaveis.enderecoServidorFtp + "img/aluno/" + Path.GetFileName(variaveis.fotoAluno);
                        try
                        {
                            ftp.EnviarArquivoFtp(variaveis.caminhoFotoAluno, urlEnviarArquivo, variaveis.usuarioFtp, variaveis.senhaFtp);
                        }
                        catch
                        {
                            MessageBox.Show("Foto não foi selecionada ou existente no servidor.", "FOTO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            catch (Exception erro)
            {
                MessageBox.Show("Erro ao alterar a foto do aluno.\n\n" + erro);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            new frmAluno().Show();
            Close();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Clear();
            txtTelefone.Clear();
            cmbStatus.SelectedIndex = -1;
            txtEmail.Clear();
            txtSenha.Clear();
            pctAluno.Image = null;
        }

        private void frmCadAluno_Load(object sender, EventArgs e)
        {
            if (variaveis.funcao == "CADASTRAR")
            {
                lblCadAluno.Text = "Cadastro de Aluno";
            }
            else if (variaveis.funcao == "ALTERAR")
            {
                lblCadAluno.Text = "Alteração de Aluno";
                CarregarDadosAluno();
            }
        }

        private void btnInserirFoto_Click(object sender, EventArgs e)
        {
            try
            {

                // Abrir o diálogo para selecionar a foto
                OpenFileDialog ofdFoto = new OpenFileDialog();
                ofdFoto.Multiselect = false;
                ofdFoto.FileName = "";
                ofdFoto.InitialDirectory = @"C:";
                ofdFoto.Title = "SELECIONE UMA FOTO";
                ofdFoto.Filter = "JPG ou PNG (*.jpg ou *.png)|*.jpg;*.png";
                ofdFoto.CheckFileExists = true;
                ofdFoto.CheckPathExists = true;
                ofdFoto.RestoreDirectory = true;

                DialogResult result = ofdFoto.ShowDialog();
                if (result == DialogResult.OK)
                {
                    pctAluno.Image = Image.FromFile(ofdFoto.FileName);
                    if (variaveis.funcao == "CADASTRAR")
                    {
                        banco.Conectar();

                        // Corrigir a consulta para obter o próximo ID
                        string selecionarId = "SELECT MAX(idAluno) + 1 FROM tbl_aluno";
                        MySqlCommand cmd = new MySqlCommand(selecionarId, banco.conexao);

                        // Executar a consulta e obter o próximo ID
                        object resultado = cmd.ExecuteScalar();
                        int proximoId = resultado != DBNull.Value ? Convert.ToInt32(resultado) : 1;

                        banco.Desconectar();
                        variaveis.fotoAluno = "aluno/" + proximoId + "_" + Regex.Replace(txtNome.Text, @"\s", "").ToLower() + ".png";
                    }
                    else
                    {
                        banco.Conectar();
                        string selecionar = "select * from tbl_aluno where idAluno = @codigo;";
                        MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                        cmd.Parameters.AddWithValue("@codigo", variaveis.codigoAluno);
                        MySqlDataReader dr = cmd.ExecuteReader();
                        variaveis.fotoAluno = "aluno/" + variaveis.codigoAluno + "_" + Regex.Replace(txtNome.Text, @"\s", "").ToLower() + ".png";
                        banco.Desconectar();
                    }
                    

                    try
                    {
                        variaveis.atFotoAluno = "S";
                        variaveis.caminhoFotoAluno = ofdFoto.FileName;
                    }
                    catch (SecurityException erro)
                    {
                        MessageBox.Show("Erro de segurança - Fale com o Admin \n Mensagem: " + erro + "\n Detalhe: " + erro.StackTrace);
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show("Você não tem permissão. \n Detalhe: " + erro);
                    }
                }

                btnSalvar.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
                btnSalvar.Focus();
            }
        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            lblNome.ForeColor = Color.White;
            lblTelefone.ForeColor = Color.White;
            lblStatus.ForeColor = Color.White;
            lblEmail.ForeColor = Color.White;
            lblSenha.ForeColor = Color.White;

            if (txtNome.Text == "")
            {
                MessageBox.Show("Preencher o nome do cliente");
                lblNome.ForeColor = Color.Red;
                txtNome.Focus();
            }
            else if (txtTelefone.MaskCompleted == false)
            {
                MessageBox.Show("Preencher o telefone do cliente");
                lblTelefone.ForeColor = Color.Red;
                txtTelefone.Focus();
            }
            else if (cmbStatus.Text == "")
            {
                MessageBox.Show("Preencher o status do cliente");
                lblStatus.ForeColor = Color.Red;
                cmbStatus.Focus();
            }
            else if (txtEmail.Text == "")
            {
                MessageBox.Show("Preencher o email do cliente");
                lblEmail.ForeColor = Color.Red;
                txtEmail.Focus();
            }
            else if (txtSenha.Text == "")
            {
                MessageBox.Show("Preencher a senha do cliente");
                lblSenha.ForeColor = Color.Red;
                txtSenha.Focus();
            }
            else
            {
                variaveis.nomeAluno = txtNome.Text;
                variaveis.telefoneAluno = txtTelefone.Text;
                variaveis.emailAluno = txtEmail.Text;
                variaveis.senhaAluno = txtSenha.Text;
                //variaveis.fotoAluno = "aluno/" + variaveis.codigoAluno + "_" + txtNome.Text.ToLower().Replace(" ", "") + ".png";
                variaveis.altAluno = "foto " + txtNome.Text;
                variaveis.statusAluno = cmbStatus.Text;

                if (variaveis.funcao == "CADASTRAR")
                {
                    InserirAluno();
                    btnVoltar.PerformClick();
                }
                else if (variaveis.funcao == "ALTERAR")
                {
                    AlterarAluno();
                    if (variaveis.atFotoAluno == "S")
                    {
                        AlterarFotoAluno();
                    }
                }

                MessageBox.Show("Cadastrar");
            }
        }

        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtEmail.Enabled = true;
                txtEmail.Focus();
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtTelefone.Enabled = true;
                txtTelefone.Focus();
            }
        }

        private void txtTelefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                txtSenha.Enabled = true;
                txtSenha.Focus();
            }
        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                cmbStatus.Enabled = true;
                cmbStatus.Focus();
            }
        }

        private void cmbStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                btnInserirFoto.Enabled = true;
                btnInserirFoto.Focus();
            }
        }

        private void btnInserirFoto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                btnSalvar.Enabled = true;
                btnSalvar.Focus();
            }
        }
    }
}
