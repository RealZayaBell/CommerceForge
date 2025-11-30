using System;
using System.ComponentModel;
using System.Reflection;

namespace Shared.core.Functional.Extensions;

public static class EnumExtensions
{
    public static string ToDescription(this Enum value)
    {
        if (value == null)
            return string.Empty;

        var type = value.GetType();
        var name = value.ToString();

        var field = type.GetField(name);
        if (field == null)
            return name;

        var attr = field.GetCustomAttribute<DescriptionAttribute>();
        return attr?.Description ?? name;
    }
}