using lowtunning.Banco;
using lowtunning.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Net;


namespace lowtunning.Controllers
{
    public class ProdutosController : Controller
    {
        // GET: Produtos
        public ActionResult Produtos(Produtosm p)
        {

            var prod = new Produtosm();

            string nome, descricao, preco, id1;

            var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
            var connection = new MySqlConnection(connString);
            var command = connection.CreateCommand();

            connection.Open();

            command.CommandText = "select * from produto p inner join tipoprod tp on p.idtipo = tp.idtipo ";

            MySqlDataReader leitor;


            leitor = command.ExecuteReader();


            ViewBag.mensagem = "Digite o usuário e senha";
            var produtos = new List<Produtosm>();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {

                    prod.cod_prod = Convert.ToString(leitor[0]);
                    prod.nome_prod = Convert.ToString(leitor[1]);
                    prod.marca_prod = Convert.ToString(leitor[2]);
                    prod.valor_prod = Convert.ToString(leitor[3]);
                    prod.descricaotipo = Convert.ToString(leitor[8]);
                    prod.idtipo = Convert.ToString(leitor[4]);
                    prod.imagem = Convert.ToString(leitor[5]);
                    produtos.Add(new Produtosm { valor_prod = prod.valor_prod, cod_prod = prod.cod_prod, nome_prod = prod.nome_prod, marca_prod = prod.marca_prod, descricaotipo = prod.descricaotipo, idtipo = prod.idtipo, imagem = prod.imagem });
                }

                connection.Close();
                return View(produtos);
            }

            return null;


        }

        public Produtosm BuscaProduto(string id)
        {


            var prod = new Produtosm();

            string nome, descricao, preco, id1;

            var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
            var connection = new MySqlConnection(connString);
            var command = connection.CreateCommand();

            connection.Open();

            command.CommandText = "select * from produto p inner join tipoprod tp on p.idtipo = tp.idtipo where cod_prod = '" + id + "'";

            MySqlDataReader leitor;


            leitor = command.ExecuteReader();


            ViewBag.mensagem = "Digite o usuário e senha";
            var produtos = new List<Produtosm>();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {

                    prod.cod_prod = Convert.ToString(leitor[0]);
                    prod.nome_prod = Convert.ToString(leitor[1]);
                    prod.marca_prod = Convert.ToString(leitor[2]);
                    prod.valor_prod = Convert.ToString(leitor[3]);
                    prod.descricaotipo = Convert.ToString(leitor[7]);
                    prod.idtipo = Convert.ToString(leitor[4]);
                    prod.imagem = Convert.ToString(leitor[5]);

                    produtos.Add(new Produtosm { cod_prod = prod.cod_prod, nome_prod = prod.nome_prod, marca_prod = prod.marca_prod, descricaotipo = prod.descricaotipo, idtipo = prod.idtipo, caminhoimagem = prod.caminhoimagem });
                }

                connection.Close();
                return prod;
            }

            return null;
        }


        ProdutosAcoes acinsprod = new ProdutosAcoes();



        public ActionResult IncluirProduto(Produtosm insprod)
        {
            string nome = "", descricao = "", preco = "";


            if (insprod.nome_prod != null)
            {


                var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
                var connection = new MySqlConnection(connString);
                var command = connection.CreateCommand();
                var imageTypes = new string[]{
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
                };


                // Salvar a imagem para a pasta e pega o caminho
                var imagemNome = String.Format("{0:yyyyMMdd-HHmmssfff}", DateTime.Now);

                var extensao = System.IO.Path.GetExtension(insprod.caminhoimagem.FileName).ToLower();
                using (var img = System.Drawing.Image.FromStream(insprod.caminhoimagem.InputStream))
                {
                    insprod.imagem = String.Format("/ProdutoImagens/{0}{1}", imagemNome, extensao);
                    // Salva imagem 
                    SalvarNaPasta(img, insprod.imagem);
                }


                connection.Open();

                command.CommandText = "insert into Produto (nome_prod,marca_prod,valor_prod,caminhoimagem,idtipo) values  ( ' " + insprod.nome_prod.ToString() + "', '" + insprod.marca_prod.ToString() + "', " + insprod.valor_prod.ToString() + ",' " + insprod.imagem.ToString() + "',(select idtipo from TipoProd where descricaotipo = '" + insprod.idtipo.ToString() + "'))";

                string inserir = "foi mulheque";


                command.Prepare();

                command.ExecuteNonQuery();


                connection.Close();


                return RedirectToAction("Produtos", "Produtos");


            }
            return View();
        }
        // POST: Produtos/Delete/5
        //[HttpPost, ActionName("IncluirProduto")]

        //public ActionResult IncluirPro(Produtosm prod)
        //{
        //    string teste;

        //  //  teste = DeletaProduto(id.ToString());

        //    return RedirectToAction("Produtos", "Produtos");
        //}

        private void SalvarNaPasta(Image img, string caminho)
        {
            using (System.Drawing.Image novaImagem = new Bitmap(img))
            {
                novaImagem.Save(Server.MapPath(caminho), img.RawFormat);
            }
        }

        public ActionResult ExcluirProd(int? id)

        {
            string teste;
            var prod1 = new Produtosm();

            prod1 = BuscaProduto(id.ToString());



            return View(prod1);
            // return View();
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("ExcluirProd")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Produtosm prod, int id)
        {
            string teste;

            teste = DeletaProduto(id.ToString());

            return RedirectToAction("Produtos", "Produtos");
        }


        public ActionResult Detalhes(int? id)
        {
            string teste;
            var prod1 = new Produtosm();

            prod1 = BuscaProduto(id.ToString());



            return View(prod1);
            // return View();
        }
        [HttpPost, ActionName("Detalhes")]
        [ValidateAntiForgeryToken]
        public ActionResult AddCarrinho(Produtosm prod, int id)
        {
            string teste;

            teste = IncluiCarrinho(id.ToString());

            return RedirectToAction("Carrinho", "Carrinho");
        }

        public string IncluiCarrinho(string id)
        {
            string inserir = "";

            if (id != null)
            {
                Produtosm produtos = new Produtosm();

                var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
                var connection = new MySqlConnection(connString);
                var command = connection.CreateCommand();



                connection.Open();

                try
                {
                    inserir = "OK";
                    command.CommandText = "insert into Carrinho (cod_prod,codvenda,prodqtd) values (" + id.ToString() + ", " + Session["codvenda"] + ",1)";

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




        public ActionResult EditarProd(Produtosm editprod)
        {
            string nome = "", descricao = "", preco = "";

            if (editprod.cod_prod != null)
            {


                var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
                var connection = new MySqlConnection(connString);
                var command = connection.CreateCommand();



                connection.Open();

                // command.CommandText = "update produto set nome_prod = '" + editprod.nome_prod.ToString() + "', marca_prod = '" + editprod.marca_prod.ToString() + "',valor_prod = '" + editprod.valor_prod.ToString() + "', select * from tipoprod descricaotipo ='" + editprod.idtipo.ToString() + "' where ' cod_prod '= '" + editprod.cod_prod.ToString()+ "')";


                command.CommandText = "update produto set nome_prod = '" + editprod.nome_prod.ToString() + "',";
                command.CommandText += "marca_prod = '" + editprod.marca_prod.ToString() + "',";
                command.CommandText += "valor_prod = '" + editprod.valor_prod.ToString() + "',";
                command.CommandText += "idtipo = (select idtipo from tipoprod where descricaotipo = '" + editprod.idtipo.ToString() + "') ";
                command.CommandText += " where cod_prod = '" + editprod.cod_prod.ToString() + "'";


                string inserir = "foi mulheque";

                command.Prepare();

                command.ExecuteNonQuery();


                connection.Close();


                return RedirectToAction("Index", "Home");

            }


            return View();


        }





        public string Updateprod(Produtosm prod, int id)
        {
            string nome = "", descricao = "", preco = "";

            if (id.ToString() != null)
            {


                var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
                var connection = new MySqlConnection(connString);
                var command = connection.CreateCommand();


                prod.valor_prod = prod.valor_prod.ToString().Replace(',', '.');
                connection.Open();

                // command.CommandText = "update produto set nome_prod = '" + editprod.nome_prod.ToString() + "', marca_prod = '" + editprod.marca_prod.ToString() + "',valor_prod = '" + editprod.valor_prod.ToString() + "', select * from tipoprod descricaotipo ='" + editprod.idtipo.ToString() + "' where ' cod_prod '= '" + editprod.cod_prod.ToString()+ "')";


                command.CommandText = "update produto set nome_prod = '" + prod.nome_prod.ToString() + "',";
                command.CommandText += "marca_prod = '" + prod.marca_prod.ToString() + "',";
                command.CommandText += "valor_prod = '" + prod.valor_prod.ToString() + "'";
                command.CommandText += " where cod_prod = '" + id.ToString() + "'";


                string inserir = "foi mulheque";

                command.Prepare();

                command.ExecuteNonQuery();


                connection.Close();


                return nome;

            }


            return null;


        }








        public ActionResult EditProduto(int? id)
        {
            string teste;
            var prod1 = new Produtosm();

            prod1 = BuscaProduto(id.ToString());



            return View(prod1);
        }
        [HttpPost, ActionName("EditProduto")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Produtosm prod, int id)
        {
            string nome, marca, valor;

            nome = prod.nome_prod;
            marca = prod.marca_prod;
            valor = prod.valor_prod;



            Updateprod(prod, id);


            return RedirectToAction("Produtos", "Produtos");
        }




        public string DeletaProduto(string id)
        {
            string inserir = "";

            if (id != null)
            {
                Produtosm produtos = new Produtosm();

                var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
                var connection = new MySqlConnection(connString);
                var command = connection.CreateCommand();



                connection.Open();

                try
                {
                    inserir = "OK";
                    command.CommandText = "delete from produto where cod_prod ='" + id.ToString() + "'";
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

    }
}