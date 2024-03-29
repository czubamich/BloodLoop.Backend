﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using BloodCore.Domain;
using BloodCore.Extensions;

namespace BloodCore.AspNet
{
    public class StronglyTypedIdConverter<TIdentity> : TypeConverter where TIdentity : Identity
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var stringValue = value as string;
            if (!string.IsNullOrEmpty(stringValue) && GuidExtensions.TryParseShort(stringValue, out var guidValue))
            {
                return (TIdentity)Activator.CreateInstance(type: typeof(TIdentity),
                    bindingAttr: BindingFlags.NonPublic | BindingFlags.Instance,
                    binder: null,
                    args: new object[] { guidValue },
                    culture: null);
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}