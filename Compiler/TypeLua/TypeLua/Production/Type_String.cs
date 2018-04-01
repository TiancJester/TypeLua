
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Types;

    // <type> ::= string
    public class Type_String : Type_basisproduction
    {
        public Token<string> String;

        public Type_String(Project project, Class @class, GOLD.Token token0)
        {
            this.String = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.String);
        }

        public override Type GetTLType(PackagesContext packagesContext)
        {
            return Type.String;
        }
    }
}
        