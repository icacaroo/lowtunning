using lowtunning.Interfaces;
using lowtunning.Interfaces.Repositories;
using lowtunning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lowtunning.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Usuarios GetById(int id)
        {

            var retorno = _usuarioRepository.GetById(id);
            return retorno;
        }

    }
}