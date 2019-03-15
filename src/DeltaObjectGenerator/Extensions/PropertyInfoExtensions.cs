﻿using DeltaObjectGenerator.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DeltaObjectGenerator.Extensions
{
    internal static class PropertyInfoExtensions
    {
        public static bool IgnoreDeltaOnDefault(this PropertyInfo propertyInfo,
            List<PropertyInfo> propertiesToIgnoreOnDefault, object newValue)
        {
            if (!propertiesToIgnoreOnDefault.Contains(propertyInfo))
            {
                return false;
            }

            if (!(newValue is IComparable comparableNewValue) || comparableNewValue == null)
            {
                return true;
            }

            var comparableDefaultValue = propertyInfo.PropertyType.GetComparableDefaultValue();

            return comparableNewValue.CompareTo(comparableDefaultValue) == 0;
        }

        public static string GetAlias(this PropertyInfo propertyInfo)
        {
            return propertyInfo
                .GetCustomAttribute<DeltaObjectAliasAttribute>()
                ?.Alias;
        }
    }
}
