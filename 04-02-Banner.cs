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
    public partial class frmCadBanner : Form
    {
        public frmCadBanner()
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
            WebClient ftpBanner = new WebClient();
            ftpBanner.Credentials = new NetworkCredential(variaveis.usuarioFtp, variaveis.senhaFtp);
            try
            {

                byte[] imageToByte = ftpBanner.DownloadData(caminhoArquivoFtp);
                return imageToByte;
            }
            catch
            {

                byte[] imageToByte = ftpBanner.DownloadData("ftp://u283879542.teachercamila@smpsistema.com.br/admin/img/banner/sem-foto.jpg");
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

        private void InserirBanner()
        {
            try
            {
                banco.Conectar();
                string inserir = "insert into tbl_banner(nomeBanner, fotoBanner, altBanner, statusBanner) values(@nome, @foto, @alt, @status);";
                MySqlCommand cmd = new MySqlCommand(inserir, banco.conexao);

                //parameters
                cmd.Parameters.AddWithValue("@nome", variaveis.nomeBanner);
                cmd.Parameters.AddWithValue("@foto", variaveis.fotoBanner);
                cmd.Parameters.AddWithValue("@alt", variaveis.altBanner);
                cmd.Parameters.AddWithValue("@status", variaveis.statusBanner);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Banner cadastrado com sucesso.", "CADASTRO DO BANNER");
                banco.Desconectar();

                if (ValidarFTP())
                {
                    if (!string.IsNullOrEmpty(variaveis.fotoBanner))
                    {
                        string urlEnviarArquivo = variaveis.enderecoServidorFtp + "img/banner/" + Path.GetFileName(variaveis.fotoBanner);
                        try
                        {
                            ftp.EnviarArquivoFtp(variaveis.caminhoBanner, urlEnviarArquivo, variaveis.usuarioFtp, variaveis.senhaFtp);
                        }
                        catch
                        {
                            MessageBox.Show("Banner não foi selecionado ou existente no servidor.", "BANNER", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao inserir banner.\n\n" + erro);
            }
        }

        private void CarregarDadosBanner()
        {
            try
            {
                banco.Conectar();
                string selecionar = "select * from tbl_banner where idBanner = @codigo;";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@codigo", variaveis.codigoBanner);
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    variaveis.nomeBanner = dr.GetString(1);
                    // Armazenar o caminho completo com o prefixo para uso interno
                    string caminhoFotoBanner = dr.GetString(2);
                    variaveis.fotoBanner = caminhoFotoBanner;

                    // Remover prefixo "banner/" apenas para exibir a imagem
                    string caminhoParaExibir = caminhoFotoBanner.Replace("banner/", "");

                    variaveis.altBanner = dr.GetString(3);
                    variaveis.statusBanner = dr.GetString(4);

                    txtNome.Text = variaveis.nomeBanner;
                    pctBanner.Image = ByteToImage(GetImgToByte(variaveis.enderecoServidorFtp + "img/banner/" + caminhoParaExibir));
                    cmbStatus.Text = variaveis.statusBanner;
                }
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar dados do banner.\n\n" + erro);
            }
        }

        private void AlterarBanner()
        {
            try
            {
                banco.Conectar();
                string inserir = "update tbl_banner set nomeBanner = @nome, fotoBanner = @foto, altBanner = @alt, statusBanner = @status where idBanner = @codigo;";
                MySqlCommand cmd = new MySqlCommand(inserir, banco.conexao);

                //parameters
                cmd.Parameters.AddWithValue("@nome", variaveis.nomeBanner);
                // Verifique se a foto foi alterada antes de atualizar
                if (variaveis.atBanner == "S")
                {
                    cmd.Parameters.AddWithValue("@foto", variaveis.fotoBanner);
                }
                else
                {
                    // Não altere o campo fotoBanner se não houver nova foto
                    string fotoAtual = ObterFotoAtual(variaveis.codigoBanner);
                    cmd.Parameters.AddWithValue("@foto", fotoAtual);
                }
                cmd.Parameters.AddWithValue("@alt", variaveis.altBanner);
                cmd.Parameters.AddWithValue("@status", variaveis.statusBanner);
                cmd.Parameters.AddWithValue("@codigo", variaveis.codigoBanner);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Banner cadastrado com sucesso.", "CADASTRO DE BANNER");
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao alterar dados do banner.\n\n" + erro);
            }
        }

        private string ObterFotoAtual(int idBanner)
        {
            try
            {
                banco.Conectar();
                string selecionar = "select fotoBanner from tbl_banner where idBanner = @codigo;";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@codigo", idBanner);
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

        private void AlterarFotoBanner()
        {
            try
            {
                banco.Conectar();
                string alterar = "update tbl_banner set fotoBanner = @foto where idBanner = @codigo;";
                MySqlCommand cmd = new MySqlCommand(alterar, banco.conexao);

                //parametros
                cmd.Parameters.AddWithValue("@foto", variaveis.fotoBanner);
                cmd.Parameters.AddWithValue("@codigo", variaveis.codigoBanner);
                //fim parametros

                cmd.ExecuteNonQuery();
                banco.Desconectar();

                if (ValidarFTP())
                {
                    if (!string.IsNullOrEmpty(variaveis.fotoBanner))
                    {
                        string urlEnviarArquivo = variaveis.enderecoServidorFtp + "img/banner/" + Path.GetFileName(variaveis.fotoBanner);
                        try
                        {
                            ftp.EnviarArquivoFtp(variaveis.caminhoBanner, urlEnviarArquivo, variaveis.usuarioFtp, variaveis.senhaFtp);
                        }
                        catch
                        {
                            MessageBox.Show("Banner não foi selecionada ou existente no servidor.", "BANNER", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao alterar a foto do banner.\n\n" + erro);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            new frmBanner().Show();
            Close();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Clear();
            pctBanner.Image = null;
            cmbStatus.SelectedIndex = -1;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            lblNome.ForeColor = Color.White;
            lblStatus.ForeColor = Color.White;

            if (txtNome.Text == "")
            {
                MessageBox.Show("Preencher o nome do banner");
                lblNome.ForeColor = Color.Red;
                txtNome.Focus();
            }
            else if (cmbStatus.Text == "")
            {
                MessageBox.Show("Preencher o status do banner");
                lblStatus.ForeColor = Color.Red;
                cmbStatus.Focus();
            }
            else
            {
                variaveis.nomeBanner = txtNome.Text;
                //variaveis.fotoBanner = "banner/" + variaveis.codigoBanner + "_" + txtNome.Text.ToLower().Replace(" ", "") + ".png";
                variaveis.altBanner = "foto" + txtNome.Text;
                variaveis.statusBanner = cmbStatus.Text;

                if (variaveis.funcao == "CADASTRAR")
                {
                    InserirBanner();
                    btnVoltar.PerformClick();
                }
                else if (variaveis.funcao == "ALTERAR")
                {
                    AlterarBanner();
                    if (variaveis.atBanner == "S")
                    {
                        AlterarFotoBanner();
                    }
                }
            }
        }

        private void frmCadBanner_Load(object sender, EventArgs e)
        {
            if (variaveis.funcao == "CADASTRAR")
            {
                lblCadBanner.Text = "Cadastro do Banner";
            }
            else if (variaveis.funcao == "ALTERAR")
            {
                lblCadBanner.Text = "Alteração do Banner";
                CarregarDadosBanner();
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
                pctBanner.Image = Image.FromFile(ofdFoto.FileName);
                if (variaveis.funcao == "CADASTRAR")
                {
                    banco.Conectar();

                    // Corrigir a consulta para obter o próximo ID
                    string selecionarId = "SELECT MAX(idBanner) + 1 FROM tbl_banner";
                    MySqlCommand cmd = new MySqlCommand(selecionarId, banco.conexao);

                    // Executar a consulta e obter o próximo ID
                    object resultado = cmd.ExecuteScalar();
                    int proximoId = resultado != DBNull.Value ? Convert.ToInt32(resultado) : 1;

                    banco.Desconectar();
                    variaveis.fotoBanner = "banner/" + proximoId + "_" + Regex.Replace(txtNome.Text, @"\s", "").ToLower() + ".png";
                }
                else
                {
                    banco.Conectar();
                    string selecionar = "select * from tbl_banner where idBanner = @codigo;";
                    MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                    cmd.Parameters.AddWithValue("@codigo", variaveis.codigoBanner);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    variaveis.fotoBanner = "banner/" + variaveis.codigoBanner + "_" + Regex.Replace(txtNome.Text, @"\s", "").ToLower() + ".png";
                    banco.Desconectar();
                }
                

                if (result == DialogResult.OK)
                {
                    try
                    {
                        variaveis.atBanner = "S";
                        variaveis.caminhoBanner = ofdFoto.FileName;
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
