using lowtunning.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lowtunning.Controllers
{
    public class CarrinhoController : Controller
    {
        // GET: Carrinho
        public ActionResult Carrinho(Carrinho c)
        {
            ////crio a lista para mandar para view 
            //var carrinho = new List<Carrinho>();
            //string teste;

            //carrinho = BuscaCarrinho();

            //return View(carrinho);

            string codvenda, prodqtd, cod_login, statusv, nome_prod, cod_prod, valor_prod, prodvenda;

            var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
            var connection = new MySqlConnection(connString);
            var command = connection.CreateCommand();

            connection.Open();

            command.CommandText = "select * from carrinho c inner join venda v on c.codvenda = v.codvenda inner join produto p  on c.cod_prod = p.cod_prod where c.codvenda = " + Session["codvenda"] + " ";


            MySqlDataReader leitor;


            leitor = command.ExecuteReader();


            ViewBag.mensagem = "Digite as informações do produto";
            var carrinho = new List<Carrinho>();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {

                    cod_prod = Convert.ToString(leitor[0]);
                    prodqtd = Convert.ToString(leitor[1]);
                    codvenda = Convert.ToString(leitor[2]);
                    cod_login = Convert.ToString(leitor[4]);
                    statusv = Convert.ToString(leitor[6]);
                    nome_prod = Convert.ToString(leitor[8]);
                    valor_prod = Convert.ToString(leitor[10]);
                    prodvenda = Convert.ToString(leitor[5]);
                    carrinho.Add(new Carrinho { cod_prod = cod_prod, prodqtd = prodqtd, codvenda = codvenda, statusv = statusv, nome_prod = nome_prod, valor_prod = valor_prod, cod_login = cod_login });
                }

                connection.Close();

                return View(carrinho);
            }

            return RedirectToAction("Produtos", "Produtos");

        }

        //public List<Carrinho> BuscaCarrinho()
        //{

        //    string codvenda, prodqtd, cod_login, statusv, nome_prod, cod_prod, valor_prod;

        //    var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
        //    var connection = new MySqlConnection(connString);
        //    var command = connection.CreateCommand();

        //    connection.Open();

        //    command.CommandText = "select * from carrinho c inner join venda v on c.codvenda = v.codvenda inner join produto p  on c.cod_prod = p.cod_prod where c.codvenda = "+ Session["codvenda"] + " ";


        //    MySqlDataReader leitor;


        //    leitor = command.ExecuteReader();


        //    ViewBag.mensagem = "Digite as informações do produto";
        //    var carrinho = new List<Carrinho>();

        //    if (leitor.HasRows)
        //    {
        //        while (leitor.Read())
        //        {

        //            cod_prod = Convert.ToString(leitor[0]);
        //            prodqtd = Convert.ToString(leitor[1]);
        //            codvenda = Convert.ToString(leitor[2]);
        //            cod_login = Convert.ToString(leitor[4]);
        //            statusv = Convert.ToString(leitor[5]);
        //            nome_prod = Convert.ToString(leitor[7]);
        //            valor_prod = Convert.ToString(leitor[9]);
        //            carrinho.Add(new Carrinho { cod_prod = cod_prod, prodqtd = prodqtd, codvenda = codvenda, statusv = statusv, nome_prod = nome_prod, valor_prod = valor_prod, cod_login = cod_login });
        //        }

        //        connection.Close();
        //        return carrinho;
        //    }

        //    return null;
        //}


        [HttpPost, ActionName("Carrinho")]
        public ActionResult AddCarrinho()
        {


            return RedirectToAction("VendaConcluida", "Venda");
        }


        public ActionResult IncluirCarrinho(Carrinho updtcarrinho)
        {
            string nome = "", descricao = "", preco = "", codvenda = ""; ;


            if (updtcarrinho.cod_prod != null)
            {


                var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
                var connection = new MySqlConnection(connString);
                var command = connection.CreateCommand();



                connection.Open();

                command.CommandText = "update Carrinho set prodqtd = '" + updtcarrinho.prodqtd.ToString() + "', codvenda = '" + updtcarrinho.codvenda.ToString() + "',cod_login = '" + updtcarrinho.cod_login.ToString() + "',statusv = '" + updtcarrinho.statusv.ToString() + "', nome_prod = '" + updtcarrinho.nome_prod.ToString() + "', valor_prod = '" + updtcarrinho.valor_prod.ToString() + "' where   = '" + updtcarrinho.codvenda.ToString() + "'";
                string inserir = "foi mulheque";


                command.Prepare();

                command.ExecuteNonQuery();


                connection.Close();


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



                    return RedirectToAction("Produtos", "Produtos");


                }
                return View();
            }  return View();
        }
    }
}
