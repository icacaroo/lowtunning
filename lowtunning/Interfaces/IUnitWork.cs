using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lowtunning.Interfaces
{
    public interface IUnitWork
    {
        /// <summary>
        /// IniciarTransacao
        /// </summary>
        void IniciarTransacao();
        /// <summary>
        /// ConfirmarTransacao
        /// </summary>
        void ConfirmarTransacao();
        /// <summary>
        /// ReverterTransacao
        /// </summary>
        void ReverterTransacao();
        /// <summary>
        /// ObterConexao
        /// </summary>
        /// <returns></returns>
        SqlConnection ObterConexao();
        /// <summary>
        /// ObterTransacao
        /// </summary>
        /// <returns></returns>
        IDbTransaction ObterTransacao();
    }
}
