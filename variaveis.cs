using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teacherCamilaCodeDev
{
    internal class variaveis
    {
        //Estrutura FTP
        public static string enderecoServidorFtp = "ftp://u283879542.teachercamila@smpsistema.com.br/admin/";
        public static string usuarioFtp = "u283879542.teachercamila";
        public static string senhaFtp = "Senac@teachercamila01";

        //Geral
        public static string funcao;
        public static int linhaSelecionada;

        //Menu
        public static string usuario, senha;
        public static int tentativa;

        //Aluno
        public static int codigoAluno, idAluno;
        public static string nomeAluno, telefoneAluno, emailAluno, senhaAluno, fotoAluno, altAluno, atFotoAluno, caminhoFotoAluno, statusAluno;

        //Atividade
        public static string arquivoAtividade, dataEnvAtividade, DataRecAtividade, dataPrevAtividade, statusAtividade;

        //Aula
        public static int codigoAula;
        public static string statusAula;
        public static DateTime dataAula;
        public static TimeSpan horaAula;

        //Banner
        public static int codigoBanner;
        public static string nomeBanner, fotoBanner, altBanner, atBanner, caminhoBanner, statusBanner;

        //Frase
        public static int codigoFrase;
        public static string nomeFrase, textoFrase, statusFrase;

        //Game
        public static int codigoGame;
        public static string nomeGame, linkGame, fotoGame, altGame, atFotoGame, caminhoFotoGame, statusGame, descricaoGame;
    }
}
