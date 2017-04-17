using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace testcontrols.Core.DI
{
    public static class ContainerExtensions
    {
        public static SimpleContainer Singleton<TImplementation>(this SimpleContainer container, string key = null)
        {
            return Singleton<TImplementation, TImplementation>(container, key);
        }

        public static SimpleContainer Singleton<TService, TImplementation>(this SimpleContainer container, string key = null)
            where TImplementation : TService
        {
            container.RegisterSingleton(typeof(TService), key, typeof(TImplementation));
            return container;
        }

        public static SimpleContainer PerRequest<TImplementation>(this SimpleContainer container, string key = null)
        {
            return PerRequest<TImplementation, TImplementation>(container, key);
        }

        public static SimpleContainer PerRequest<TService, TImplementation>(this SimpleContainer container, string key = null)
            where TImplementation : TService
        {
            container.RegisterPerRequest(typeof(TService), key, typeof(TImplementation));
            return container;
        }

        public static SimpleContainer Instance<TService>(this SimpleContainer container, TService instance)
        {
            container.RegisterInstance(typeof(TService), null, instance);
            return container;
        }

        public static SimpleContainer Handler<TService>(this SimpleContainer container,
                                                        Func<SimpleContainer, object> handler)
        {
            container.RegisterHandler(typeof(TService), null, handler);
            return container;
        }

        public static SimpleContainer AllTypesOf<TService>(this SimpleContainer container, Assembly assembly,
                                                           Func<Type, bool> filter = null)
        {
            if (filter == null)
                filter = type => true;

            var serviceType = typeof(TService);
            var types = from type in assembly.GetTypes()
                        where serviceType.IsAssignableFrom(type)
                              && !type.IsAbstract()
                              && !type.IsInterface()
                              && filter(type)
                        select type;

            foreach (var type in types)
            {
                container.RegisterSingleton(typeof(TService), null, type);
            }

            return container;
        }

        public static TService GetInstance<TService>(this SimpleContainer container, string key = null)
        {
            return (TService)container.GetInstance(typeof(TService), key);
        }

        public static IEnumerable<TService> GetAllInstances<TService>(this SimpleContainer container)
        {
            return container.GetAllInstances(typeof(TService)).Cast<TService>();
        }

        public static bool HasHandler<TService>(this SimpleContainer container, string key = null)
        {
            return container.HasHandler(typeof(TService), key);
        }

        public static void UnregisterHandler<TService>(this SimpleContainer container, string key = null)
        {
            container.UnregisterHandler(typeof(TService), key);
        }
    }
}
