
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Types;

    // <type> ::= any
    public class Type_Any : Type_basisproduction
    {
        public Token<string> Any;

        public Type_Any(Project project, Class @class, GOLD.Token token0)
        {
            this.Any = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Any);
        }

        public override Type GetTLType(PackagesContext packagesContext)
        {
            return Type.Any;
        }
    }
}
        