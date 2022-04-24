using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lowtunning.Banco
{
    public class conexao
    {
        MySqlConnection cn = new MySqlConnection("Server=localhost;DataBase=lowtunning;User=root;pwd=1234567");
        public static string msg;
        public MySqlConnection MyConectarBD()
        {
            try
            {
                cn.Open();
            }
            catch (Exception erro)
            {
                msg = "Ocorreu um erro ao se conectar" + erro.Message;
            }
            return cn;
        }
        public MySqlConnection MyDesconectarBD()
        {
            try
            {
                cn.Close();
            }
            catch (Exception erro)
            {
                msg = "Ocorreu um erro ao se conectar" + erro.Message;
            }
            return cn;
        }
    }


    //MySqlCommand cmd = new MySqlCommand(" select* from Usuarios where NomeUsuario = @Nome and Senha = @Senha ");
    //con.MyConectarBD();
    //cmd.Parameters.Add(" @Email", MySqlDbType.VarChar).Value = user.NomeUsuario;
    //cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = user.Senha;
    //MySqlDataReader leitor;
    //leitor = cmd.ExecuteReader();
    //if (leitor.HasRows)
    //{
    //    while (leitor.Read())
    //    {
    //        {
    //            user.NomeUsuario = Convert.ToString(leitor["NomeUsuario "]);
    //            user.Senha = Convert.ToString(leitor["Senha"]);
    //        }
    //    }
    //}
    //else
    //{
    //    user.NomeUsuario = null;
    //    user.Senha = null;
    //}
    //con.MyDesconectarBD();
}



