using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testcontrols.Core.DI
{
    public class Container : SimpleContainer
    {
        private static Lazy<Container> _container = new Lazy<Container>(() => new Container());

        public static Container Instance
        {
            get { return _container.Value; }
        }

        public static T GetInstance<T>()
        {
            return (T)Instance.GetInstance(typeof(T), null);
        }
    }
}
