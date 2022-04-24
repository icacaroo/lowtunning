using lowtunning.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lowtunning.Banco
{
    public class LoginAcoes
    {
        conexao con = new conexao();

        public static string mail;

        public void TestarUsuario(Usuarios user)
        {

            var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
            var connection = new MySqlConnection(connString);
            var command = connection.CreateCommand();
             

            connection.Open();
            command.CommandText = " select * from Login where usuario = '@Nome' and Senha = '@Senha'";
             
            command.ExecuteNonQuery();


            connection.Close();

        }


        public static string incluifunc;

        public void IncluirUsuario(Usuarios user)
        {

            var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
            var connection = new MySqlConnection(connString);
            var command = connection.CreateCommand();



            connection.Open();
            command.CommandText = "insert into Login (usuario,Senha,email,telefone,cpf,endereço,status) values (' @Nome',@Senha,@email,@telefone,@cpf,@endereço,@status)";
            // command.ExecuteNonQuery();

            connection.Close();

        }

        public void ExcluirFunc(Usuarios user)
        {

            var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
            var connection = new MySqlConnection(connString);
            var command = connection.CreateCommand();



            connection.Open();
            command.CommandText = "delete from Login where cod_login =' @codlogin')";
            // command.ExecuteNonQuery();

            connection.Close();

        }
    }

}





