
namespace TypeLua.Production
{
    using System;
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project.Exception;

    // <modifier list>
    public class Modifier_list_basisproduction : Production
    {
        public virtual List<Token<TypeLuaParser.SymbolIndex>> GetModifiers(List<Token<TypeLuaParser.SymbolIndex>> modifiers)
        {
            return modifiers;
        }

        public static bool ModifierVerify(List<Token<TypeLuaParser.SymbolIndex>> modifiers, params TypeLuaParser.SymbolIndex[] max)
        {
            Dictionary<TypeLuaParser.SymbolIndex, bool> maxCollection = new Dictionary<TypeLuaParser.SymbolIndex, bool>();
            Dictionary<TypeLuaParser.SymbolIndex, bool> modifierCollection = new Dictionary<TypeLuaParser.SymbolIndex, bool>();

            foreach (var symbolIndex in max)
            {
                maxCollection[symbolIndex] = true;
            }

            int access = 0;
            foreach (var symbolIndex in modifiers)
            {
                if (symbolIndex.Symbol == TypeLuaParser.SymbolIndex.Public || symbolIndex.Symbol == TypeLuaParser.SymbolIndex.Private || symbolIndex.Symbol == TypeLuaParser.SymbolIndex.Protected)
                {
                    if (++access > 1)
                    {
                        throw new SyntaxException(
                            "More then one access defined here",
                            symbolIndex.Line,
                            symbolIndex.Column);
                    }
                }
                if (!maxCollection.ContainsKey(symbolIndex.Symbol))
                {
                    throw new SyntaxException(
                        string.Format("Invalid '{0}'", Enum.GetName(typeof(TypeLuaParser.SymbolIndex), symbolIndex.Symbol)),
                        symbolIndex.Line,
                        symbolIndex.Column);
                }
                if (modifierCollection.ContainsKey(symbolIndex.Symbol))
                {
                    throw new SyntaxException(
                        string.Format("More then one '{0}' here", Enum.GetName(typeof(TypeLuaParser.SymbolIndex), symbolIndex.Symbol)),
                        symbolIndex.Line,
                        symbolIndex.Column);
                }
                else
                {
                    modifierCollection.Add(symbolIndex.Symbol, true);
                }
            }

            return true;
        }
    }
}
        