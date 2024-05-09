using System;
using System.Reflection;

namespace Elvenwood
{
    public class Singleton<T> where T: Singleton<T>
    {
        private static T _mInstance;

        public static T Instance
        {
            get
            {
                if (_mInstance == null)
                {
                    var type = typeof(T);
                    var ctors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                    var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
                    if (ctor == null)
                    {
                        throw new Exception("Non Public Constructor Not Found in " + type.Name);
                    }

                    _mInstance = ctor.Invoke(null) as T;
                }

                return _mInstance;
            }
        }
    }
}