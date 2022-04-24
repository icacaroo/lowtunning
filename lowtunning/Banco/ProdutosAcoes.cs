using lowtunning.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lowtunning.Banco
{
    public class ProdutosAcoes
    {
        public static string incluiprod;

        public void IncluirUsuario(Produtosm user)
        {

            var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
            var connection = new MySqlConnection(connString);
            var command = connection.CreateCommand();



            connection.Open();
            command.CommandText = "insert into Produto (nome_prod,marca_prod,valor_prod,descricaotipo) values (' @ome_prod','@marca_prod','@valor_prod','@descricaotipo')";
            // command.ExecuteNonQuery();

            connection.Close();

        }

    }
}