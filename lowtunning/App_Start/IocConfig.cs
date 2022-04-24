using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lowtunning.Interfaces;
using lowtunning.Interfaces.Repositories;
using lowtunning.Repositories;
using lowtunning.Services;
using lowtunning.Services.Connection;
using Ninject;
using Ninject.Syntax;

namespace lowtunning.App_Start
{
    public class IocConfig
    {
        public static void ConfigurarDependencias()
        {
            //Cria o Container
            IKernel kernel = new StandardKernel();

            //Instrução para mapear a interface IPessoa para a classe concreta Pessoa
            kernel.Bind<IUsuarioService>().To<UsuarioService>();
            kernel.Bind<IUsuarioRepository>().To<UsuarioRepository>();
          //  kernel.Bind<IUnitWork>().To<UnitWork>();

            //Registra o container no ASP.NET
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IResolutionRoot _resolutionRoot;

        public NinjectDependencyResolver(IResolutionRoot kernel)
        {
            _resolutionRoot = kernel;
        }

        public object GetService(Type serviceType)
        {
            return _resolutionRoot.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _resolutionRoot.GetAll(serviceType);
        }

       
    }
   
}