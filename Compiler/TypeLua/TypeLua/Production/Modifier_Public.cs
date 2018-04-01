
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <modifier> ::= public
    public class Modifier_Public : Modifier_basisproduction
    {
        public Token<string> Public;

        public Modifier_Public(Project project, Class @class, GOLD.Token token0)
        {
            this.Public = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Public);
        }

        public override Token<TypeLuaParser.SymbolIndex> GetModifier()
        {
            return new Token<TypeLuaParser.SymbolIndex>(TypeLuaParser.SymbolIndex.Public, this.Public.Line, this.Public.Column);
        }
    }
}
        