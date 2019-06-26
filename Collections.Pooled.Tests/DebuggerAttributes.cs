// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.Diagnostics
{
    internal class DebuggerAttributeInfo
    {
        public object Instance { get; set; }
        public IEnumerable<PropertyInfo> Properties { get; set; }
    }

    internal static class DebuggerAttributes
    {
        internal static object GetFieldValue(object obj, string fieldName) => GetField(obj, fieldName).GetValue(obj);

        internal static void InvokeDebuggerTypeProxyProperties(object obj)
        {
            var info = ValidateDebuggerTypeProxyProperties(obj.GetType(), obj);
            foreach (var pi in info.Properties)
            {
                pi.GetValue(info.Instance, null);
            }
        }

        internal static DebuggerAttributeInfo ValidateDebuggerTypeProxyProperties(object obj) => ValidateDebuggerTypeProxyProperties(obj.GetType(), obj);

        internal static DebuggerAttributeInfo ValidateDebuggerTypeProxyProperties(Type type, object obj) => ValidateDebuggerTypeProxyProperties(type, type.GenericTypeArguments, obj);

        internal static DebuggerAttributeInfo ValidateDebuggerTypeProxyProperties(Type type, Type[] genericTypeArguments, object obj)
        {
            var proxyType = GetProxyType(type, genericTypeArguments);

            // Create an instance of the proxy type, and make sure we can access all of the instance properties 
            // on the type without exception
            object proxyInstance = Activator.CreateInstance(proxyType, obj);
            var properties = GetDebuggerVisibleProperties(proxyType);
            return new DebuggerAttributeInfo
            {
                Instance = proxyInstance,
                Properties = properties
            };
        }

        public static DebuggerBrowsableState? GetDebuggerBrowsableState(MemberInfo info)
        {
            var debuggerBrowsableAttribute = info.CustomAttributes
                .SingleOrDefault(a => a.AttributeType == typeof(DebuggerBrowsableAttribute));
            // Enums in attribute constructors are boxed as ints, so cast to int? first.
            return (DebuggerBrowsableState?)(int?)debuggerBrowsableAttribute?.ConstructorArguments.Single().Value;
        }

        public static IEnumerable<FieldInfo> GetDebuggerVisibleFields(Type debuggerAttributeType)
        {
            // The debugger doesn't evaluate non-public members of type proxies.
            var visibleFields = debuggerAttributeType.GetFields()
                .Where(fi => fi.IsPublic && GetDebuggerBrowsableState(fi) != DebuggerBrowsableState.Never);
            return visibleFields;
        }

        public static IEnumerable<PropertyInfo> GetDebuggerVisibleProperties(Type debuggerAttributeType)
        {
            // The debugger doesn't evaluate non-public members of type proxies. GetGetMethod returns null if the getter is non-public.
            var visibleProperties = debuggerAttributeType.GetProperties()
                .Where(pi => pi.GetGetMethod() != null && GetDebuggerBrowsableState(pi) != DebuggerBrowsableState.Never);
            return visibleProperties;
        }

        public static object GetProxyObject(object obj) => Activator.CreateInstance(GetProxyType(obj), obj);

        public static Type GetProxyType(object obj) => GetProxyType(obj.GetType());

        public static Type GetProxyType(Type type) => GetProxyType(type, type.GenericTypeArguments);

        private static Type GetProxyType(Type type, Type[] genericTypeArguments)
        {
            // Get the DebuggerTypeProxyAttibute for obj
            var attrs =
                type.GetTypeInfo().CustomAttributes
                .Where(a => a.AttributeType == typeof(DebuggerTypeProxyAttribute))
                .ToArray();
            if (attrs.Length != 1)
            {
                throw new InvalidOperationException($"Expected one DebuggerTypeProxyAttribute on {type}.");
            }
            var cad = attrs[0];

            var proxyType = cad.ConstructorArguments[0].ArgumentType == typeof(Type) ?
                (Type)cad.ConstructorArguments[0].Value :
                Type.GetType((string)cad.ConstructorArguments[0].Value);
            if (genericTypeArguments.Length > 0)
            {
                proxyType = proxyType.MakeGenericType(genericTypeArguments);
            }

            return proxyType;
        }

        internal static string ValidateDebuggerDisplayReferences(object obj)
        {
            // Get the DebuggerDisplayAttribute for obj
            var objType = obj.GetType();
            var attrs =
                objType.GetTypeInfo().CustomAttributes
                .Where(a => a.AttributeType == typeof(DebuggerDisplayAttribute))
                .ToArray();
            if (attrs.Length != 1)
            {
                throw new InvalidOperationException($"Expected one DebuggerDisplayAttribute on {objType}.");
            }
            var cad = attrs[0];

            // Get the text of the DebuggerDisplayAttribute
            string attrText = (string)cad.ConstructorArguments[0].Value;

            string[] segments = attrText.Split(new[] { '{', '}' });

            if (segments.Length % 2 == 0)
            {
                throw new InvalidOperationException($"The DebuggerDisplayAttribute for {objType} lacks a closing brace.");
            }

            if (segments.Length == 1)
            {
                throw new InvalidOperationException($"The DebuggerDisplayAttribute for {objType} doesn't reference any expressions.");
            }

            var sb = new StringBuilder();

            for (int i = 0; i < segments.Length; i += 2)
            {
                string literal = segments[i];
                sb.Append(literal);

                if (i + 1 < segments.Length)
                {
                    string reference = segments[i + 1];
                    bool noQuotes = reference.EndsWith(",nq");

                    reference = reference.Replace(",nq", String.Empty);

                    // Evaluate the reference.
                    if (!TryEvaluateReference(obj, reference, out object member))
                    {
                        throw new InvalidOperationException($"The DebuggerDisplayAttribute for {objType} contains the expression \"{reference}\".");
                    }

                    string memberString = GetDebuggerMemberString(member, noQuotes);

                    sb.Append(memberString);
                }
            }

            return sb.ToString();
        }

        private static string GetDebuggerMemberString(object member, bool noQuotes)
        {
            string memberString = "null";
            if (member != null)
            {
                memberString = member.ToString();
                if (member is string)
                {
                    if (!noQuotes)
                    {
                        memberString = '"' + memberString + '"';
                    }
                }
                else if (!IsPrimitiveType(member))
                {
                    memberString = '{' + memberString + '}';
                }
            }

            return memberString;
        }

        private static bool IsPrimitiveType(object obj) =>
            obj is byte || obj is sbyte ||
            obj is short || obj is ushort ||
            obj is int || obj is uint ||
            obj is long || obj is ulong ||
            obj is float || obj is double;

        private static bool TryEvaluateReference(object obj, string reference, out object member)
        {
            var pi = GetProperty(obj, reference);
            if (pi != null)
            {
                member = pi.GetValue(obj);
                return true;
            }

            var fi = GetField(obj, reference);
            if (fi != null)
            {
                member = fi.GetValue(obj);
                return true;
            }

            member = null;
            return false;
        }

        private static FieldInfo GetField(object obj, string fieldName)
        {
            for (var t = obj.GetType(); t != null; t = t.GetTypeInfo().BaseType)
            {
                var fi = t.GetTypeInfo().GetDeclaredField(fieldName);
                if (fi != null)
                {
                    return fi;
                }
            }
            return null;
        }

        private static PropertyInfo GetProperty(object obj, string propertyName)
        {
            for (var t = obj.GetType(); t != null; t = t.GetTypeInfo().BaseType)
            {
                var pi = t.GetTypeInfo().GetDeclaredProperty(propertyName);
                if (pi != null)
                {
                    return pi;
                }
            }
            return null;
        }
    }
}
