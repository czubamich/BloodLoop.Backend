using System;
using System.Linq;
using System.Reflection;
using BloodCore.Domain;

namespace BloodCore.AspNet
{
    public static class StronglyTypedIdTypeDescriptor
    {
        public static void AddStronglyTypedIdConverter(Assembly assembly, Action<Type> additionalAction)
        {
            assembly
                .ExportedTypes
                .Where(x => !x.IsGenericTypeDefinition && !x.IsAbstract && x.BaseType.BaseType == typeof(Identity))
                .ToList().ForEach(idType =>
                {
                    additionalAction?.Invoke(idType);
                });
        }
    }
}