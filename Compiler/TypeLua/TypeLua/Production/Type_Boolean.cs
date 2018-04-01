
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Types;

    // <type> ::= boolean
    public class Type_Boolean : Type_basisproduction
    {
        public Token<string> Boolean;

        public Type_Boolean(Project project, Class @class, GOLD.Token token0)
        {
            this.Boolean = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Boolean);
        }

        public override Type GetTLType(PackagesContext packagesContext)
        {
            return Type.Bool;
        }
    }
}
        