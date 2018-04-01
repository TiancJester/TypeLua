
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <modifier> ::= private
    public class Modifier_Private : Modifier_basisproduction
    {
        public Token<string> Private;

        public Modifier_Private(Project project, Class @class, GOLD.Token token0)
        {
            this.Private = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Private);
        }

        public override Token<TypeLuaParser.SymbolIndex> GetModifier()
        {
            return new Token<TypeLuaParser.SymbolIndex>(TypeLuaParser.SymbolIndex.Private, this.Private.Line, this.Private.Column);
        }
    }
}
        