
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Types;

    // <type> ::= table
    public class Type_Table : Type_basisproduction
    {
        public Token<string> Table;

        public Type_Table(Project project, Class @class, GOLD.Token token0)
        {
            this.Table = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Table);
        }

        public override Type GetTLType(PackagesContext packagesContext)
        {
            return Type.Table;
        }
    }
}
        