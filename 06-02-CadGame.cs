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
    public partial class frmCadGame : Form
    {
        public frmCadGame()
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
            WebClient ftpGame = new WebClient();
            ftpGame.Credentials = new NetworkCredential(variaveis.usuarioFtp, variaveis.senhaFtp);
            try
            {

                byte[] imageToByte = ftpGame.DownloadData(caminhoArquivoFtp);
                return imageToByte;
            }
            catch
            {

                byte[] imageToByte = ftpGame.DownloadData("ftp://u283879542.teachercamila@smpsistema.com.br/admin/img/game/sem-foto.jpg");
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

        private void InserirGame()
        {
            try
            {
                banco.Conectar();
                string inserir = "insert into tbl_game(nomeGame, linkGame, fotoGame, altGame, statusGame, descricaoGame) values(@nome, @link, @foto, @alt, @status, @descricao);";
                MySqlCommand cmd = new MySqlCommand(inserir, banco.conexao);

                //parameters
                cmd.Parameters.AddWithValue("@nome", variaveis.nomeGame);
                cmd.Parameters.AddWithValue("@link", variaveis.linkGame);
                cmd.Parameters.AddWithValue("@foto", variaveis.fotoGame);
                cmd.Parameters.AddWithValue("@alt", variaveis.altGame);
                cmd.Parameters.AddWithValue("@status", variaveis.statusGame);
                cmd.Parameters.AddWithValue("@descricao", variaveis.descricaoGame);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Game cadastrado com sucesso.", "CADASTRO DO GAME");
                banco.Desconectar();

                if (ValidarFTP())
                {
                    if (!string.IsNullOrEmpty(variaveis.fotoGame))
                    {
                        string urlEnviarArquivo = variaveis.enderecoServidorFtp + "img/game/" + Path.GetFileName(variaveis.fotoGame);
                        try
                        {
                            ftp.EnviarArquivoFtp(variaveis.caminhoFotoGame, urlEnviarArquivo, variaveis.usuarioFtp, variaveis.senhaFtp);
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
                MessageBox.Show("Erro ao inserir game.\n\n" + erro);
            }
        }

        private void CarregarDadosGame()
        {
            try
            {
                banco.Conectar();
                string selecionar = "select * from tbl_game where idGame = @codigo;";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@codigo", variaveis.codigoGame);
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    variaveis.nomeGame = dr.GetString(1);
                    variaveis.linkGame = dr.GetString(2);
                    // Armazenar o caminho completo com o prefixo para uso interno
                    string caminhoFotoGame = dr.GetString(3);
                    variaveis.fotoGame = caminhoFotoGame;

                    // Remover prefixo "game/" apenas para exibir a imagem
                    string caminhoParaExibir = caminhoFotoGame.Replace("game/", "");

                    variaveis.altGame = dr.GetString(4);
                    variaveis.statusGame = dr.GetString(5);
                    variaveis.descricaoGame = dr.GetString(6);

                    txtNome.Text = variaveis.nomeGame;
                    txtLink.Text = variaveis.linkGame;
                    pctFoto.Image = ByteToImage(GetImgToByte(variaveis.enderecoServidorFtp + "img/game/" + caminhoParaExibir));
                    cmbStatus.Text = variaveis.statusGame;
                    txtDescricao.Text = variaveis.descricaoGame;
                }
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar dados do game.\n\n" + erro);
            }
        }

        private void AlterarGame()
        {
            try
            {
                banco.Conectar();
                string inserir = "update tbl_game set nomeGame = @nome, linkGame = @link, fotoGame = @foto, altGame = @alt, statusGame = @status, descricaoGame = @descricao where idGame = @codigo;";
                MySqlCommand cmd = new MySqlCommand(inserir, banco.conexao);

                //parameters
                cmd.Parameters.AddWithValue("@nome", variaveis.nomeGame);
                cmd.Parameters.AddWithValue("@link", variaveis.linkGame);
                // Verifique se a foto foi alterada antes de atualizar
                if (variaveis.atFotoGame == "S")
                {
                    cmd.Parameters.AddWithValue("@foto", variaveis.fotoGame);
                }
                else
                {
                    // Não altere o campo fotoGame se não houver nova foto
                    string fotoAtual = ObterFotoAtual(variaveis.codigoGame);
                    cmd.Parameters.AddWithValue("@foto", fotoAtual);
                }
                cmd.Parameters.AddWithValue("@alt", variaveis.altGame);
                cmd.Parameters.AddWithValue("@status", variaveis.statusGame);
                cmd.Parameters.AddWithValue("@descricao", variaveis.descricaoGame);
                cmd.Parameters.AddWithValue("@codigo", variaveis.codigoGame);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Game alterado com sucesso.", "ALTERAÇÃO DO GAME");
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao alterar dados do game.\n\n" + erro);
            }
        }

        private string ObterFotoAtual(int idGame)
        {
            try
            {
                banco.Conectar();
                string selecionar = "select fotoGame from tbl_game where idGame = @codigo;";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@codigo", idGame);
                object resultado = cmd.ExecuteScalar();
                banco.Desconectar();

                return resultado != DBNull.Value ? resultado.ToString() : "";
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao obter a foto atual do banner.\n\n" + erro);
                return "";
            }
        }

        private void AlterarFotoGame()
        {
            try
            {
                banco.Conectar();
                string alterar = "update tbl_game set fotoGame = @foto where idGame = @codigo;";
                MySqlCommand cmd = new MySqlCommand(alterar, banco.conexao);

                //parametros
                cmd.Parameters.AddWithValue("@foto", variaveis.fotoGame);
                cmd.Parameters.AddWithValue("@codigo", variaveis.codigoGame);

                cmd.ExecuteNonQuery();
                banco.Desconectar();

                if (ValidarFTP())
                {
                    if (!string.IsNullOrEmpty(variaveis.fotoGame))
                    {
                        string urlEnviarArquivo = variaveis.enderecoServidorFtp + "img/game/" + Path.GetFileName(variaveis.fotoGame);
                        try
                        {
                            ftp.EnviarArquivoFtp(variaveis.caminhoFotoGame, urlEnviarArquivo, variaveis.usuarioFtp, variaveis.senhaFtp);
                        }
                        catch
                        {
                            MessageBox.Show("Game não foi selecionada ou existente no servidor.", "FOTO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao alterar a foto do game.\n\n" + erro);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            new frmGame().Show();
            Close();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Clear();
            txtLink.Clear();
            pctFoto.Image = null;
            cmbStatus.SelectedIndex = -1;
            txtDescricao.Clear();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            lblNome.ForeColor = Color.White;
            lblStatus.ForeColor = Color.White;
            lblLink.ForeColor = Color.White;

            if (txtNome.Text == "")
            {
                MessageBox.Show("Preencher o nome do game");
                lblNome.ForeColor = Color.Red;
                txtNome.Focus();
            }
            else if (txtLink.Text == "")
            {
                MessageBox.Show("Preencher o link do game");
                lblLink.ForeColor = Color.Red;
                txtLink.Focus();
            }
            else if (cmbStatus.Text == "")
            {
                MessageBox.Show("Preencher o status do game");
                lblStatus.ForeColor = Color.Red;
                cmbStatus.Focus();
            }
            else if(txtDescricao.Text == "")
            {
                MessageBox.Show("Preencher a descrição do game");
                lblDescricao.ForeColor = Color.Red;
                txtDescricao.Focus();
            }
            else
            {
                variaveis.nomeGame = txtNome.Text;
                variaveis.linkGame = txtLink.Text;
                //variaveis.fotoAluno = "aluno/" + variaveis.codigoAluno + "_" + txtNome.Text.ToLower().Replace(" ", "") + ".png";
                variaveis.altGame = "foto" + txtNome.Text;
                variaveis.statusGame = cmbStatus.Text;
                variaveis.descricaoGame = txtDescricao.Text;

                if (variaveis.funcao == "CADASTRAR")
                {
                    InserirGame();
                    btnVoltar.PerformClick();
                }
                else if (variaveis.funcao == "ALTERAR")
                {
                    AlterarGame();
                    if (variaveis.atFotoGame == "S")
                    {
                        AlterarFotoGame();
                    }
                }
            }
        }

        private void frmCadGame_Load(object sender, EventArgs e)
        {
            if (variaveis.funcao == "CADASTRAR")
            {
                lblCadGame.Text = "Cadastro do Game";
            }
            else if (variaveis.funcao == "ALTERAR")
            {
                lblCadGame.Text = "Alteração do Game";
                CarregarDadosGame();
            }
        }

        private void btnInserirFoto_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofdFoto = new OpenFileDialog();
                ofdFoto.Multiselect = false;
                ofdFoto.FileName = "";
                ofdFoto.InitialDirectory = @"C:";
                ofdFoto.Title = "SELECIONE UMA FOTO";
                ofdFoto.Filter = "JPG ou PNG (*.jpg ou (*.png)|*.jpg;*.png";
                ofdFoto.CheckFileExists = true;
                ofdFoto.CheckPathExists = true;
                ofdFoto.RestoreDirectory = true;

                DialogResult result = ofdFoto.ShowDialog();
                pctFoto.Image = Image.FromFile(ofdFoto.FileName);
                if (variaveis.funcao == "CADASTRAR")
                {
                    banco.Conectar();

                    // Corrigir a consulta para obter o próximo ID
                    string selecionarId = "SELECT MAX(idGame) + 1 FROM tbl_game";
                    MySqlCommand cmd = new MySqlCommand(selecionarId, banco.conexao);

                    // Executar a consulta e obter o próximo ID
                    object resultado = cmd.ExecuteScalar();
                    int proximoId = resultado != DBNull.Value ? Convert.ToInt32(resultado) : 1;

                    banco.Desconectar();
                    variaveis.fotoGame = "game/" + proximoId + "_" + Regex.Replace(txtNome.Text, @"\s", "").ToLower() + ".png";
                }
                else
                {
                    banco.Conectar();
                    string selecionar = "select * from tbl_banner where idBanner = @codigo;";
                    MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                    cmd.Parameters.AddWithValue("@codigo", variaveis.codigoBanner);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    variaveis.fotoGame = "game/" + variaveis.codigoGame + "_" + Regex.Replace(txtNome.Text, @"\s", "").ToLower() + ".png";
                    banco.Desconectar();
                }
                

                if (result == DialogResult.OK)
                {
                    try
                    {
                        variaveis.atFotoGame = "S";
                        variaveis.caminhoFotoGame = ofdFoto.FileName;
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
            catch
            {
                btnSalvar.Focus();
            }
        }
    }
}
