using Autofac.Builder;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autofac {
    public static class ContainerBuilderExtensions {

        public static IRegistrationBuilder<Task<T>, SimpleActivatorData, SingleRegistrationStyle> RegisterAsync<T>(this ContainerBuilder This)
            where T : notnull {

            var ret = This.Register(async Context => {
                var Provider = Context.Resolve<T>();

                if(Provider is IInitializeAsync V1) {
                    await V1.InitializeAsync()
                        .DefaultAwait()
                        ;
                }
                
                return Provider;
            });

            return ret;
        }

    }

}
