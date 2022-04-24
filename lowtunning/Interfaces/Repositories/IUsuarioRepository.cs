using lowtunning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lowtunning.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Usuarios GetById(int id);
    }
}
