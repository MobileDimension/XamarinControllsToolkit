﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace testcontrols.Core.DI
{
    internal static class ReflectionExtensions
    {
        public static bool IsAssignableFrom(this Type t, Type c)
        {
            return t.GetTypeInfo().IsAssignableFrom(c.GetTypeInfo());
        }

        public static Type[] GetGenericArguments(this Type t)
        {
            return t.GetTypeInfo().GenericTypeArguments;
        }

        public static IEnumerable<ConstructorInfo> GetConstructors(this Type t)
        {
            return t.GetTypeInfo().DeclaredConstructors;
        }

        public static IEnumerable<Type> GetTypes(this Assembly a)
        {
            return a.DefinedTypes.Select(t => t.AsType());
        }

        public static bool IsAbstract(this Type t)
        {
            return t.GetTypeInfo().IsAbstract;
        }

        public static bool IsInterface(this Type t)
        {
            return t.GetTypeInfo().IsInterface;
        }

        public static bool IsGenericType(this Type t)
        {
            return t.GetTypeInfo().IsGenericType;
        }

        public static MethodInfo GetMethod(this Type t, string name, Type[] parameters)
        {
            return t.GetRuntimeMethod(name, parameters);
        }
    }
}
