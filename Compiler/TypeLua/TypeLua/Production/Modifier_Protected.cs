
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <modifier> ::= protected
    public class Modifier_Protected : Modifier_basisproduction
    {
        public Token<string> Protected;

        public Modifier_Protected(Project project, Class @class, GOLD.Token token0)
        {
            this.Protected = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Protected);
        }

        public override Token<TypeLuaParser.SymbolIndex> GetModifier()
        {
            return new Token<TypeLuaParser.SymbolIndex>(TypeLuaParser.SymbolIndex.Protected, this.Protected.Line, this.Protected.Column);
        }
    }
}
        