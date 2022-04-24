using lowtunning.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lowtunning.Controllers
{
    public class VendaController : Controller
    {
        // GET: Venda
        public ActionResult Venda()
        {
            return View();
        }


        public string ControlEstoque(string qtd , int prod)
        {
            string nome = "", descricao = "", preco = "";

            if (qtd != null)
            {


                var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
                var connection = new MySqlConnection(connString);
                var command = connection.CreateCommand();



                connection.Open();

                // command.CommandText = "update produto set nome_prod = '" + editprod.nome_prod.ToString() + "', marca_prod = '" + editprod.marca_prod.ToString() + "',valor_prod = '" + editprod.valor_prod.ToString() + "', select * from tipoprod descricaotipo ='" + editprod.idtipo.ToString() + "' where ' cod_prod '= '" + editprod.cod_prod.ToString()+ "')";


                command.CommandText = "update produto  set quantidade  = quantidade -  " + prod.ToString() + " where cod_prod = " + qtd + "";


                string inserir = "foi mulheque";

                command.Prepare();

                command.ExecuteNonQuery();


                connection.Close();


                return nome;

            }


            return nome;


        }






        public ActionResult VendaConcluida(Venda updtvenda)
        {
            string nome = "", descricao = "", preco = "",codvenda  = "", cod_prod = "";
            int qtd = 0;


            var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
            var connection = new MySqlConnection(connString);
            var command = connection.CreateCommand();



            connection.Open();

            command.CommandText = "update Venda set statusv = 1 where codvenda =  " + Session["codvenda"] + "";

            string inserir = "foi mulheque";

            command.Prepare();

            command.ExecuteNonQuery();


            connection.Close();

            connection.Open();
            // faz aqui o update na tabela estoque ;

            var command3 = connection.CreateCommand();

 
            command3.CommandText = "select count(*),cod_prod from carrinho where codvenda =   " + Session["codvenda"] + " group by cod_prod";

            MySqlDataReader leitor2;


            leitor2 = command3.ExecuteReader();


            if (leitor2.HasRows)
            {
                var command5 = connection.CreateCommand();
                while (leitor2.Read())
                {
                    cod_prod = (leitor2[0].ToString());
                    qtd = (Convert.ToInt32(leitor2[1]));


                    ControlEstoque(qtd.ToString(), Convert.ToInt32(cod_prod));


                }

                connection.Close();
 
            }



            // ja etm um sleect que pega as quantidaeds dos prosutos e o cod ;





            connection.Open();

            var command1 = connection.CreateCommand();

            command1.CommandText = "insert into Venda (statusv,cod_login) values  (0," + Session["cod_login"] + ")";

            command1.Prepare();

            command1.ExecuteNonQuery();

            var command2 = connection.CreateCommand();


            command2.CommandText = "select max(codvenda) from venda";
            MySqlDataReader leitor1;


            leitor1 = command2.ExecuteReader();


            if (leitor1.HasRows)
            {
                while (leitor1.Read())
                {
                    codvenda = (leitor1[0].ToString());

                    Session["codvenda"] = codvenda.ToString();

                }

                connection.Close();


                return RedirectToAction("Index", "Home");
 

            }
 

            return RedirectToAction("Index", "Home");

        }
    }
}
