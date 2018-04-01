// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>04/02/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project.Types
{
    using System;
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;

    [Flags]
    public enum AccessType
    {
        Any = 1,
        Private = 2,
        Public = 4,
        Protected = 8,
        Static = 16,
        Global = 32,
        Extern = 64,
    }

    public static class AccessTypeHelper
    {
        public static AccessType GetAccessType(this TypeLuaParser.SymbolIndex symbol)
        {
            switch (symbol)
            {
                case TypeLuaParser.SymbolIndex.Extern: return AccessType.Extern;
                case TypeLuaParser.SymbolIndex.Private: return AccessType.Private;
                case TypeLuaParser.SymbolIndex.Public: return AccessType.Public;
                case TypeLuaParser.SymbolIndex.Protected: return AccessType.Protected;
                case TypeLuaParser.SymbolIndex.Static: return AccessType.Static;
                case TypeLuaParser.SymbolIndex.Global: return AccessType.Global;
            }
            return AccessType.Any;
        }

        public static AccessType GetAccessType(this List<Token<TypeLuaParser.SymbolIndex>> symbols)
        {
            AccessType accessType = 0;
            foreach (var symbol in symbols)
            {
                var access = symbol.Symbol.GetAccessType();
                accessType |= access;
            }
            return accessType;
        }

        public static bool IsInRange(this AccessType access, AccessType maxRange)
        {
            int range = (int)(access | maxRange);
            return ((int)(maxRange)) >= range;
        }
    }
}