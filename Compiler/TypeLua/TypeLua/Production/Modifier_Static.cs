
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <modifier> ::= static
    public class Modifier_Static : Modifier_basisproduction
    {
        public Token<string> Static;

        public Modifier_Static(Project project, Class @class, GOLD.Token token0)
        {
            this.Static = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Static);
        }

        public override Token<TypeLuaParser.SymbolIndex> GetModifier()
        {
            return new Token<TypeLuaParser.SymbolIndex>(TypeLuaParser.SymbolIndex.Static, this.Static.Line, this.Static.Column);
        }
    }
}
        