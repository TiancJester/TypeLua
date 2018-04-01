
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <modifier> ::= global
    public class Modifier_Global : Modifier_basisproduction
    {
        public Token<string> Global;

        public Modifier_Global(Project project, Class @class, GOLD.Token token0)
        {
            this.Global = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Global);
        }

        public override Token<TypeLuaParser.SymbolIndex> GetModifier()
        {
            return new Token<TypeLuaParser.SymbolIndex>(TypeLuaParser.SymbolIndex.Global, this.Global.Line, this.Global.Column);
        }
    }
}
        