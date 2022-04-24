using lowtunning.Interfaces;
using lowtunning.Models;
using lowtunning.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lowtunning.Controllers
{
    [System.Web.Mvc.Route("Usuario")]
    public class UsuarioController : Controller 
    {
        private readonly IUsuarioService _servicoUsuario;


        public UsuarioController(IUsuarioService servicoUsuario)
        {
            _servicoUsuario = servicoUsuario;

        }
       
        public System.Web.Mvc.ActionResult GetById([FromRoute] int id)
        {

            var teste = "";
            var usuario = _servicoUsuario.GetById(id);
            return View(usuario);

        }

        
        public System.Web.Mvc.ActionResult Login( )
        {


            var teste = "teste";
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

                      return RedirectToAction("Index", "Home");

                    //  return View();

        }
    }
}