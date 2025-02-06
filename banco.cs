using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace teacherCamilaCodeDev
{
    internal class banco
    {
        //string de conexao banco de dados
        //para banco de dados local (xampp)
        //public static string db = "SERVER=localhost;USER=root;DATABASE=dbautomestre";


        //para banco de dados online (servidor web (externo))
        public static string db = "SERVER=exemple.com.br;USER=root;PASSWORD=password;DATABASE=localhost;SSL MODE=None";

        public static MySqlConnection conexao;

        //método para abrir a conexão
        public static void Conectar()
        {
            try
            {
                conexao = new MySqlConnection(db);
                conexao.Open();
            }
            catch
            {
                MessageBox.Show("Erro ao conectar com o banco de dados");
            }
        }

        //método para fechar a conexão
        public static void Desconectar()
        {
            try
            {
                conexao = new MySqlConnection(db);
                conexao.Close();
            }
            catch
            {
                MessageBox.Show("Erro ao desconectar com o banco de dados");
            }
        }
    }
}
