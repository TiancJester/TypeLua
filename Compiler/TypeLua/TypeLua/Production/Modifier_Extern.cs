
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <modifier> ::= extern
    public class Modifier_Extern : Modifier_basisproduction
    {
        public Token<string> Extern;

        public Modifier_Extern(Project project, Class @class, GOLD.Token token0)
        {
            this.Extern = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Extern);
        }

        public override Token<TypeLuaParser.SymbolIndex> GetModifier()
        {
            return new Token<TypeLuaParser.SymbolIndex>(TypeLuaParser.SymbolIndex.Extern, this.Extern.Line, this.Extern.Column);
        }
    }
}
        