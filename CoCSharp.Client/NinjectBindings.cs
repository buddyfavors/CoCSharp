using CoCSharp.Client.Services;
using Ninject.Modules;

namespace CoCSharp.Client
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IProxyService>().To<JsonSocketProxyService>();
        }
    }
}
