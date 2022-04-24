using lowtunning.Banco;
using lowtunning.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace lowtunning.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        LoginAcoes acesso = new LoginAcoes();



        public ActionResult Pedido()
        {
            var prod = new Produtosm();

            string nome, descricao, preco, id1,qtd,vl,cd_venda;


            var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
            var connection = new MySqlConnection(connString);
            var command = connection.CreateCommand();

            connection.Open();

            command.CommandText = " select count(*),c.*,sum(valor_prod) from carrinho c" +
                                  " inner join venda v on c.codvenda = v.codvenda" +
                                  " inner join produto p  on c.cod_prod = p.cod_prod" +
                                  " where cod_login = " + Session["cod_login"] + " and statusv = 1 group by codvenda";

            MySqlDataReader leitor;


            leitor = command.ExecuteReader();


            ViewBag.mensagem = "Digite o usuário e senha";
            var produtos = new List<Produtosm>();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {

                    qtd= Convert.ToString(leitor[0]);
                   // vl = Convert.ToString(leitor[4]);
                    vl = Convert.ToString(leitor[6]);
                    cd_venda = Convert.ToString(leitor[3]);
                    produtos.Add(new Produtosm { valor_prod= vl, qtd = qtd , cd_venda  = cd_venda });
                }

                connection.Close();
                return View(produtos);
            }

            return null;
        }


        public ActionResult GerenciarPedido()
        {
            var prod = new Produtosm();

            string nome, descricao, preco, id1, qtd, vl, cd_venda,usuario;


            var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
            var connection = new MySqlConnection(connString);
            var command = connection.CreateCommand();

            connection.Open();

            command.CommandText = " select count(*),c.*,l.usuario,sum(valor_prod) from carrinho c" +
                                  " inner join venda v on c.codvenda = v.codvenda" +
                                  " inner join produto p  on c.cod_prod = p.cod_prod" +
                                  " inner join login l  on l.cod_login = v.cod_login" +
                                  " where statusv = 1 group by codvenda";

            MySqlDataReader leitor;


            leitor = command.ExecuteReader();


            ViewBag.mensagem = "Digite o usuário e senha";
            var produtos = new List<Produtosm>();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {

                    qtd = Convert.ToString(leitor[2]);
                    // vl = Convert.ToString(leitor[4]);
                    vl = Convert.ToString(leitor[7]);
                    cd_venda = Convert.ToString(leitor[3]);
                    usuario = Convert.ToString(leitor[6]);
                    produtos.Add(new Produtosm { valor_prod = vl, qtd = qtd, cd_venda = cd_venda, usuario = usuario });
                }

                connection.Close();
                return View(produtos);
            }

            return null;
        }

        public ActionResult ProdutoPedido(int? id)
        {
            var prod = new Produtosm();

            string nome, descricao, preco, id1, qtd, vl, cd_venda,prodvenda;


            var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
            var connection = new MySqlConnection(connString);
            var command = connection.CreateCommand();

            connection.Open();

            command.CommandText = " select p.* , c.* from carrinho c" +
                                  " inner join venda v on c.codvenda = v.codvenda" +
                                  " inner join produto p  on c.cod_prod = p.cod_prod" +
                                  " where c.codvenda= " + id.ToString() + " and statusv = 1";

            MySqlDataReader leitor;


            leitor = command.ExecuteReader();


            ViewBag.mensagem = "Digite o usuário e senha";
            var produtos = new List<Produtosm>();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {

                    nome = Convert.ToString(leitor[1]);
                    vl = Convert.ToString(leitor[3]);
                    cd_venda = Convert.ToString(leitor[8]);
                    prodvenda = Convert.ToString(leitor[9]);

                    produtos.Add(new Produtosm { valor_prod = vl, nome_prod = nome, cd_venda = cd_venda , prodvenda = prodvenda });
                }

                connection.Close();
                return View(produtos);
            }

            return null;
        }


        public ActionResult MarcarProd(int? id)
        {
            var prod = new Produtosm();

            string nome, descricao, preco, id1, qtd, vl, cd_venda, data;


            var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
            var connection = new MySqlConnection(connString);
            var command = connection.CreateCommand();

            connection.Open();

            command.CommandText = " select p.* , c.* from carrinho c" +
                                  " inner join venda v on c.codvenda = v.codvenda" +
                                  " inner join produto p  on c.cod_prod = p.cod_prod" +
                                  " where prodvenda = " + id.ToString() + " and statusv = 1";

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
                    prod.imagem = Convert.ToString(leitor[4]);
                    prod.prodvenda = Convert.ToString(leitor[9]);
                    prod.data = Convert.ToString(leitor[10]);    



                    produtos.Add(new Produtosm { prodvenda = prod.prodvenda, cod_prod = prod.cod_prod, nome_prod = prod.nome_prod, marca_prod = prod.marca_prod, descricaotipo = prod.descricaotipo, idtipo = prod.idtipo, caminhoimagem = prod.caminhoimagem, data  = prod.data });
                }

              
                return View(prod);
                connection.Close();
            }

            return null;
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("MarcarProd")]
        [ValidateAntiForgeryToken]
        public ActionResult MarcaConfirmed(Produtosm prod, int id)
        {
            string teste;

            teste = UpdateMarca(prod, id);

            return RedirectToAction("Produtos", "Produtos");
        }

        public string UpdateMarca(Produtosm marca, int id)
        {
            string usuario = "", cpf, endereço;

            if (id.ToString() != null)
            {


                var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
                var connection = new MySqlConnection(connString);
                var command = connection.CreateCommand();


                connection.Open();

                // command.CommandText = "update produto set nome_prod = '" + editprod.nome_prod.ToString() + "', marca_prod = '" + editprod.marca_prod.ToString() + "',valor_prod = '" + editprod.valor_prod.ToString() + "', select * from tipoprod descricaotipo ='" + editprod.idtipo.ToString() + "' where ' cod_prod '= '" + editprod.cod_prod.ToString()+ "')";


                command.CommandText = "update carrinho set dt_prod = '" + marca.data.ToString() + "'";
                command.CommandText += " where prodvenda = '" + id.ToString() + "'";


                string inserir = "foi mulheque";

                command.Prepare();

                command.ExecuteNonQuery();


                connection.Close();


                return usuario;

            }


            return null;


        }

        public string Login(Usuarios verLogin)
        {
            #region Comentado_Nao_Lembro_o_PQ
            //string usuraio = "", senha = "", id = "", status = "", codvenda = "";

            //acesso.TestarUsuario(verLogin);


            //if (verLogin.usuario != null)
            //{


            //    var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
            //    var connection = new MySqlConnection(connString);
            //    var command = connection.CreateCommand();



            //    connection.Open();

            //    command.CommandText = " select* from Login where usuario = '" + verLogin.usuario.ToString() + "' and Senha = '" + verLogin.Senha.ToString() + "' ";

            //    string goiaba = "icaro";


            //    MySqlDataReader leitor;


            //    leitor = command.ExecuteReader();


            //    ViewBag.mensagem = "Digite o usuário e senha";

            //    if (leitor.HasRows)
            //    {
            //        while (leitor.Read())
            //        {
            //            id = Convert.ToString(leitor[0]);
            //            usuraio = Convert.ToString(leitor[1]);
            //            senha = Convert.ToString(leitor[2]);
            //            status = Convert.ToString(leitor[7]);

            //            FormsAuthentication.SetAuthCookie(verLogin.usuario, false);
            //            Session["cod_login"] = id.ToString();
            //            Session["usuarioLogado"] = usuraio.ToString();
            //            Session["status"] = status.ToString();


            //        }

            //        connection.Close();

            //        connection.Open();

            //        var command1 = connection.CreateCommand();

            //        command1.CommandText = "insert into Venda (statusv,cod_login) values  (0,"+ Session["cod_login"]+")";

            //        command1.Prepare();

            //        command1.ExecuteNonQuery();

            //        var command2 = connection.CreateCommand();


            //        command2.CommandText = "select max(codvenda) from venda";
            //        MySqlDataReader leitor1;


            //        leitor1 = command2.ExecuteReader();


            //        if (leitor1.HasRows)
            //        {
            //            while (leitor1.Read())
            //            {
            //                codvenda = (leitor1[0].ToString());

            //                Session["codvenda"] = codvenda.ToString();

            //            }

            //            connection.Close();


            //            return RedirectToAction("Index", "Home");

            //            return View();

            //        }

            //    }
            //    return View();
            //}
            #endregion
            
            return null;
        }


        LoginAcoes acusu = new LoginAcoes();

        public ActionResult IncluirUsuario(Usuarios insusu)
        {
            string nome = "", descricao = "", preco = "";


            if (insusu.usuario != null)
            {


                var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
                var connection = new MySqlConnection(connString);
                var command = connection.CreateCommand();



                connection.Open();

                command.CommandText = "insert into Login (usuario,Senha,email,telefone,cpf,endereço,status) values  ( ' " + insusu.usuario.ToString() + "', '" + insusu.Senha.ToString() + "', '" + insusu.email.ToString() + "', '" + insusu.telefone.ToString() + "', '" + insusu.cpf.ToString() + "', '" + insusu.endereço.ToString() + "','3')";

                string inserir = "foi mulheque";


                command.Prepare();

                command.ExecuteNonQuery();


                connection.Close();


                return RedirectToAction("Index", "Home");


            }
            return View();
        }

        LoginAcoes acfunc = new LoginAcoes();

        public ActionResult IncluirFunc(Usuarios insfunc)
        {
            string nome = "", descricao = "", preco = "";


            if (insfunc.usuario != null)
            {


                var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
                var connection = new MySqlConnection(connString);
                var command = connection.CreateCommand();



                connection.Open();

                command.CommandText = "insert into Login (usuario,Senha,email,telefone,cpf,endereço,status) values  ( ' " + insfunc.usuario.ToString() + "', '" + insfunc.Senha.ToString() + "', '" + insfunc.email.ToString() + "', '" + insfunc.telefone.ToString() + "', '" + insfunc.cpf.ToString() + "', '" + insfunc.endereço.ToString() + "','2')";

                string inserir = "foi mulheque";


                command.Prepare();

                command.ExecuteNonQuery();


                connection.Close();


                return RedirectToAction("Index", "Home");


            }
            return View();
        }


        public ActionResult Funcionarios(Usuarios u)
        {

            //crio a lista para mandar para view 
            var usuarios = new List<Usuarios>();
            string teste;


            usuarios = BuscaUsuario(" ", 2);

            return View(usuarios);

        }

        public List<Usuarios> BuscaUsuario(string id, int con)
        {

            string cod_login, usuario, senha, endereço,cpf, email,telefone;

            var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
            var connection = new MySqlConnection(connString);
            var command = connection.CreateCommand();

            connection.Open();

            command.CommandText = "select * from Login  ";

            if (con != 1)
            { command.CommandText += " where status in (2,1)"; }
   

            MySqlDataReader leitor;


            leitor = command.ExecuteReader();


            var usuarios = new List<Usuarios>();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {

                    cod_login = Convert.ToString(leitor[0]);
                    usuario = Convert.ToString(leitor[1]);
                    senha = Convert.ToString(leitor[2]);
                    email = Convert.ToString(leitor[3]);
                    telefone = Convert.ToString(leitor[4]);
                    cpf = Convert.ToString(leitor[5]);
                    endereço = Convert.ToString(leitor[6]);
                    usuarios.Add(new Usuarios { cod_login = cod_login, usuario = usuario, Senha = senha, email = email, telefone = telefone, cpf = cpf, endereço =endereço});
                }

                connection.Close();
                return usuarios;
            }

            return null;
        }

        public Usuarios BuscaUsuarioUni(int? id)
        {

            string cod_login, usuario, senha, endereço, cpf, email, telefone;

            var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
            var connection = new MySqlConnection(connString);
            var command = connection.CreateCommand();

            connection.Open();

            command.CommandText = "select * from Login  where cod_login = '" + id + "' ";


            MySqlDataReader leitor;


            leitor = command.ExecuteReader();


            var usuarios = new Usuarios();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {

                    usuarios.cod_login = Convert.ToString(leitor[0]);
                    usuarios.usuario = Convert.ToString(leitor[1]);
                    usuarios.Senha = Convert.ToString(leitor[2]);
                    usuarios.email = Convert.ToString(leitor[3]);
                    usuarios.telefone = Convert.ToString(leitor[4]);
                    usuarios.cpf = Convert.ToString(leitor[5]);
                    usuarios.endereço = Convert.ToString(leitor[6]);
                    //usuarios.Add(new Usuarios { cod_login = cod_login, usuario = usuario, Senha = senha, email = email, telefone = telefone, cpf = cpf, endereço = endereço });
                }

                connection.Close();
                return usuarios;
            }

            return null;
        }

        public ActionResult ExcluirFunc(int? id)
        {
            string teste;
            var user = new Usuarios();

            user = BuscaUsuarioUni(id);



            return View(user);
        }

        [HttpPost, ActionName("ExcluirFunc")]
        [ValidateAntiForgeryToken]
        public ActionResult Delet(Usuarios user, int id)
        {
            string usuario, cpf, endereço, email, telefone;

            usuario = user.usuario;
            cpf = user.cpf;
            endereço = user.endereço;
            email = user.email;
            telefone = user.telefone;



            DeletaFunc(user, id);


            return RedirectToAction("Funcionarios", "Home");
        }
        public string DeletaFunc(Usuarios user, int id)
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
                    command.CommandText = "delete from Login where cod_login ='" + id.ToString() + "'";
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

        public ActionResult EditFuncionario(int? id)
        {
            string teste;
            var user = new Usuarios();

            user = BuscaUsuarioUni(id);



            return View(user);
        }

        [HttpPost, ActionName("EditFuncionario")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuarios user, int id)
        {
            string usuario, cpf, endereço,email,telefone;

            usuario = user.usuario;
            cpf = user.cpf;
            endereço = user.endereço;
            email = user.email;
            telefone = user.telefone;



            UpdateFunc(user, id);


            return RedirectToAction("Funcionarios", "Home");
        }

        public string UpdateFunc(Usuarios user, int id)
        {
            string usuario="", cpf, endereço;

            if (id.ToString() != null)
            {


                var connString = "Server=localhost;DataBase=bd_lowTunning;User=root;pwd=1234567";
                var connection = new MySqlConnection(connString);
                var command = connection.CreateCommand();


                connection.Open();

                // command.CommandText = "update produto set nome_prod = '" + editprod.nome_prod.ToString() + "', marca_prod = '" + editprod.marca_prod.ToString() + "',valor_prod = '" + editprod.valor_prod.ToString() + "', select * from tipoprod descricaotipo ='" + editprod.idtipo.ToString() + "' where ' cod_prod '= '" + editprod.cod_prod.ToString()+ "')";


                command.CommandText = "update Login set usuario = '" + user.usuario.ToString() + "',";
                command.CommandText += "cpf = '" + user.cpf.ToString() + "',";
                command.CommandText += "endereço = '" + user.endereço.ToString() + "'";
                command.CommandText += "email = '" + user.email.ToString() + "'";
                command.CommandText += "telefone = '" + user.telefone.ToString() + "'";
                command.CommandText += " where cod_login = '" + id.ToString() + "'";


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

    
