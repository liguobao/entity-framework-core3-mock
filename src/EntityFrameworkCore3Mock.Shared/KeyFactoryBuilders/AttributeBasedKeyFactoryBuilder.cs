﻿/*
 * Copyright 2017-2019 Wouter Huysentruit
 *
 * See LICENSE file.
 */

using System;
using System.Linq;
using System.Reflection;
using EntityFrameworkCore3Mock.Shared.KeyFactoryBuilders;

namespace EntityFrameworkCore3Mock
{
    public sealed class AttributeBasedKeyFactoryBuilder<TAttribute> : KeyFactoryBuilderBase
        where TAttribute : Attribute
    {
        protected override PropertyInfo[] ResolveKeyProperties<T>()
        {
            var entityType = typeof(T);
            var keyProperties = entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.GetCustomAttribute(typeof(TAttribute)) != null)
                .ToArray();
            if (!keyProperties.Any()) throw new InvalidOperationException($"Entity type {entityType.Name} does not contain any property marked with {typeof(TAttribute).Name}");
            return keyProperties;
        }
    }
}
