
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Element;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Package;
    using TypeLua.Project.Types;

    // <parameter> ::= <type> '...' Identifier
    public class Parameter_Type_Dotdotdot_Identifier : Parameter_basisproduction
    {
        public Token<Type_basisproduction> Type;
        public Token<string> Dotdotdot;
        public Token<string> Identifier;

        public Parameter_Type_Dotdotdot_Identifier(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2)
        {
            this.Type = new Token<Type_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Type_basisproduction)token0.Data};
            this.Children.Add(this.Type);
            this.Dotdotdot = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Dotdotdot);
            this.Identifier = new Token<string>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (string)token2.Data};
            this.Children.Add(this.Identifier);

            throw new SyntaxException("Not support '...'", this.Dotdotdot.Line, this.Dotdotdot.Column);
        }

        public override Parameter GetParameter(PackagesContext packagesContext)
        {
            return new Parameter(this.Identifier.Symbol, this.Type.Symbol.GetTLType(packagesContext),true);
        }

        public override Token<string> GetIdentifierToken()
        {
            return this.Identifier;
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append("...");
        }
    }
}
        