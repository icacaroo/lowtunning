using lowtunning.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lowtunning.Controllers
{
    public class FornecedorController : Controller
    {
        // GET: Fornecedor

        public ActionResult Fornecedor(Fornecedor f)
        {

            //crio a lista para mandar para view 
            var fornecedor = new List<Fornecedor>();
            string teste;

            fornecedor = BuscaFornecedor("0=0", 1);

            return View(fornecedor);

        }

        public List<Fornecedor> BuscaFornecedor(string id, int con)
        {

            string idforn, nomeforn, descricaoforn, cnpj,email,endereço, telefone;

            var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
            var connection = new MySqlConnection(connString);
            var command = connection.CreateCommand();

            connection.Open();

            command.CommandText = "select * from Fornecedor where ";

            if (con == 1)
            { command.CommandText += id; }
            else
            { command.CommandText += "id = '" + id + "'"; }



            MySqlDataReader leitor;


            leitor = command.ExecuteReader();


            var fornecedor = new List<Fornecedor>();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {

                    idforn = Convert.ToString(leitor[0]);
                    nomeforn = Convert.ToString(leitor[1]);
                    descricaoforn = Convert.ToString(leitor[2]);
                    cnpj = Convert.ToString(leitor[3]);
                    endereço = Convert.ToString(leitor[4]);
                    email = Convert.ToString(leitor[5]);
                    telefone = Convert.ToString(leitor[6]);
                    fornecedor.Add(new Fornecedor{ idforn = idforn, nomeforn = nomeforn, descricaoforn = descricaoforn, cnpj = cnpj, endereço = endereço , email = email, telefone  = telefone });
                }

                connection.Close();
                return fornecedor;
            }

            return null;
        }
        public Fornecedor BuscaFornecedorUni(int? id)
        {

            string idforn, nomeforn, descricaoforn, endereço, cnpj, email, telefone;

            var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
            var connection = new MySqlConnection(connString);
            var command = connection.CreateCommand();

            connection.Open();

            command.CommandText = "select * from Fornecedor where idforn = '" + id + "' ";


            MySqlDataReader leitor;


            leitor = command.ExecuteReader();


            var fornecedor = new Fornecedor();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {

                    fornecedor.idforn = Convert.ToString(leitor[0]);
                    fornecedor.nomeforn = Convert.ToString(leitor[1]);
                    fornecedor.descricaoforn = Convert.ToString(leitor[2]);
                    fornecedor.cnpj = Convert.ToString(leitor[3]);
                    fornecedor.endereço = Convert.ToString(leitor[4]);
                    fornecedor.email = Convert.ToString(leitor[5]);
                    fornecedor.telefone = Convert.ToString(leitor[6]);
                    //usuarios.Add(new Usuarios { cod_login = cod_login, usuario = usuario, Senha = senha, email = email, telefone = telefone, cpf = cpf, endereço = endereço });
                }

                connection.Close();

                return fornecedor;
            }

            return null;
        }

        public ActionResult IncluirForn(Fornecedor insforn)
        {
            string nome = "", descricao = "", preco = "";


            if (insforn.nomeforn != null)
            {


                var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
                var connection = new MySqlConnection(connString);
                var command = connection.CreateCommand();



                connection.Open();

                command.CommandText = "insert into Fornecedor (nomeforn,descricaoforn,cnpj,endereço,email,telefone) values  ( ' " + insforn.nomeforn.ToString() + "', '" + insforn.descricaoforn.ToString() + "', '" + insforn.cnpj.ToString() + "', '" + insforn.endereço.ToString() + "', '" + insforn.email.ToString() + "', ' " + insforn.telefone.ToString() + "')";

                string inserir = "foi mulheque";


                command.Prepare();

                command.ExecuteNonQuery();


                connection.Close();


                return RedirectToAction("Index", "Home");


            }
            return View();
        }

        public ActionResult ExcluirForn(int? id)
        {
            string teste;
            var fornecedor = new Fornecedor();

            fornecedor = BuscaFornecedorUni(id);



            return View(fornecedor);

        }
        [HttpPost, ActionName("ExcluirForn")]
        [ValidateAntiForgeryToken]
        public ActionResult Delet(Fornecedor fornecedor, int id)
        {
            string nomeforn, cnpj, descricaoforn, endereço, email, telefone;

            nomeforn = fornecedor.nomeforn;
            descricaoforn = fornecedor.descricaoforn;
            cnpj = fornecedor.cnpj;
            endereço = fornecedor.endereço;
            email = fornecedor.email;
            telefone = fornecedor.telefone;


            UpdateForn(fornecedor, id);


            return RedirectToAction("Fornecedor", "Fornecedor");
        }


        public ActionResult EditarForn (int? id)
        {
            string teste;
            var fornecedor = new Fornecedor();

            fornecedor = BuscaFornecedorUni(id);



            return View(fornecedor);

        }
        [HttpPost, ActionName("EditarForn")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fornecedor fornecedor, int id)
        {
            string nomeforn, cnpj,descricaoforn, endereço, email, telefone;

            nomeforn = fornecedor.nomeforn;
            descricaoforn = fornecedor.descricaoforn;
            cnpj = fornecedor.cnpj;
            endereço = fornecedor.endereço;
            email = fornecedor.email;
            telefone = fornecedor.telefone;


            DeletaForn(fornecedor, id);


            return RedirectToAction("Fornecedor", "Fornecedor");
        }
        public string DeletaForn(Fornecedor fornecedor, int id)
        {
            string inserir = "";

            if (id != null)
            {
                Usuarios usuarios = new Usuarios();

                var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
                var connection = new MySqlConnection(connString);
                var command = connection.CreateCommand();



                connection.Open();

                try
                {
                    inserir = "OK";
                    command.CommandText = "delete from Fornecedor where idforn ='" + id.ToString() + "'";
                }
                catch
                {
                    inserir = "SEM CONEXAO";
                }

                command.Prepare();
                command.ExecuteNonQuery();
                connection.Close();


                return inserir;

            }

            return inserir;
        }

        public string UpdateForn(Fornecedor fornecedor, int id)
        {
            string usuario = "", cpf, endereço;

            if (id.ToString() != null)
            {


                var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
                var connection = new MySqlConnection(connString);
                var command = connection.CreateCommand();


                connection.Open();

                // command.CommandText = "update produto set nome_prod = '" + editprod.nome_prod.ToString() + "', marca_prod = '" + editprod.marca_prod.ToString() + "',valor_prod = '" + editprod.valor_prod.ToString() + "', select * from tipoprod descricaotipo ='" + editprod.idtipo.ToString() + "' where ' cod_prod '= '" + editprod.cod_prod.ToString()+ "')";


                command.CommandText = "delete from Fornecedor where idforn = '" + id.ToString() + "'";

                string inserir = "foi mulheque";

                command.Prepare();

                command.ExecuteNonQuery();


                connection.Close();


                return usuario;

            }


            return null;


        }


    }
   

}
