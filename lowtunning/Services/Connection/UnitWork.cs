using lowtunning.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace lowtunning.Services.Connection
{
    public class UnitWork  
    {
        private readonly string connectionString;
        private IDbConnection conexao;
        private IDbTransaction transacao;


        // vamos nos conectar ao SQL Server Express e à base de dados
        // locadora usando Windows Authentication
        private static string connString = "Data Source=NO0052\\SQLEXPRESS;Initial Catalog=projeto_senac;Integrated Security=true";
        // representa a conexão com o banco
        private static SqlConnection conn = null;


        // método que permite obter a conexão
       



        public UnitWork(string connectionString)
        {
            this.connectionString = "Data Source=NO0052\\SQLEXPRESS;Initial Catalog=projeto_senac;Integrated Security=true";
  
        }

        public void ConfirmarTransacao()
        {
            transacao?.Commit();
            transacao = null;
        }

        public void Dispose()
        {

            try
            {
                if (conexao != null && conexao.State == ConnectionState.Open)
                {
                    conexao.Dispose();
                }
            }
            catch
            {

            }
        }

        public void IniciarTransacao()
        {
            if (conexao == null) CriarConexao();
            transacao = conexao.BeginTransaction();
        }

        public SqlConnection ObterConexao()
        {

            // vamos criar a conexão
            conn = new SqlConnection("Data Source=NO0052\\SQLEXPRESS;Initial Catalog=projeto_senac;Integrated Security=true");

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

            return conn;
        }

        private void CriarConexao()
        {
            conexao = new SqlConnection(connectionString);
            conexao.Open();
        }

        public IDbTransaction ObterTransacao()
        {
            return transacao;
        }

        public void ReverterTransacao()
        {
            transacao?.Rollback();
            transacao = null;
        }
    }
}