
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <modifier list> ::= <modifier> <modifier list>
    public class Modifierlist_Modifier_Modifierlist : Modifier_list_basisproduction
    {
        public Token<Modifier_basisproduction> Modifier;
        public Token<Modifier_list_basisproduction> Modifierlist;

        public Modifierlist_Modifier_Modifierlist(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Modifier = new Token<Modifier_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Modifier_basisproduction)token0.Data};
            this.Children.Add(this.Modifier);
            this.Modifierlist = new Token<Modifier_list_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Modifier_list_basisproduction)token1.Data};
            this.Children.Add(this.Modifierlist);
        }

        public override List<Token<TypeLuaParser.SymbolIndex>> GetModifiers(List<Token<TypeLuaParser.SymbolIndex>> modifiers)
        {
            if (modifiers == null)
            {
                modifiers = new List<Token<TypeLuaParser.SymbolIndex>>();
            }
            var symbolIndex = this.Modifier.Symbol.GetModifier();
            if (symbolIndex != null)
            {
                modifiers.Add(symbolIndex);
            }
            this.Modifierlist.Symbol.GetModifiers(modifiers);
            return modifiers;
        }
    }
}
        