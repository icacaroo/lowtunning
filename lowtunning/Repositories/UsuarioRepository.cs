using lowtunning.Interfaces;
using lowtunning.Interfaces.Repositories;
using lowtunning.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace lowtunning.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        public Usuarios GetById(int id)
        {
            var conn = new SqlConnection("Data Source=NO0052\\SQLEXPRESS;Initial Catalog=projeto_senac;Integrated Security=true");

            // a conexão foi feita com sucesso?
            try
            {
                // abre a conexão e a devolve ao chamador do método
                conn.Open();
            }
            catch (SqlException sqle)
            {
                conn = null;
                // ops! o que aconteceu?
                // uma boa idéia aqui é gravar a exceção em um arquivo de log
            }
            var retorno = new Usuarios();
            var select = $"SELECT * from tb_usuario;";

            SqlCommand cmd = new SqlCommand(select, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                retorno.usuario = (string)dr["str_descricao"];
            }

            return retorno;
        }
    }
}