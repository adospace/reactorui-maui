using System;
using System.Collections.Generic;
using System.Text;

namespace MauiReactor.ScaffoldGenerator;

internal static class StringExtensions
{
    public static string CamelCase(this string s)
    {
        if (string.IsNullOrWhiteSpace(s))
            return s;
        return char.ToLowerInvariant(s[0]) + s.Substring(1, s.Length - 1);
    }

    public static string ToReservedWordTypeName(this string typeName)
    {
        return typeName switch
        {
            "SByte" => "sbyte",
            "Byte" => "byte",
            "Int16" => "short",
            "UInt16" => "ushort",
            "Int32" => "int",
            "UInt32" => "uint",
            "Int64" => "long",
            "UInt64" => "ulong",
            "Char" => "char",
            "Single" => "float",
            "Double" => "double",
            "Boolean" => "bool",
            "Decimal" => "decimal",
            "String" => "string",
            "Object" => "object?",
            _ => typeName,
        };
    }

    public static string ToReservedWordFullTypeName(this string fullTypeName)
    {
        return fullTypeName switch
        {
            "System.SByte" => "sbyte",
            "System.Byte" => "byte",
            "System.Int16" => "short",
            "System.UInt16" => "ushort",
            "System.Int32" => "int",
            "System.UInt32" => "uint",
            "System.Int64" => "long",
            "System.UInt64" => "ulong",
            "System.Char" => "char",
            "System.Single" => "float",
            "System.Double" => "double",
            "System.Boolean" => "bool",
            "System.Decimal" => "decimal",
            "System.String" => "string",
            "System.Object" => "object?",
            _ => fullTypeName.Replace('+', '.'),
        };
    }

    public static string ToLocalVariableName(this string varName)
    {
        varName = char.ToLowerInvariant(varName[0]) + varName.Substring(1);

        if (varName == "switch")
            return "@switch";

        return varName;
    }
}
