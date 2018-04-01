
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Types;

    // <type> ::= number
    public class Type_Number : Type_basisproduction
    {
        public Token<string> Number;

        public Type_Number(Project project, Class @class, GOLD.Token token0)
        {
            this.Number = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Number);
        }

        public override Type GetTLType(PackagesContext packagesContext)
        {
            return Type.Number;
        }
    }
}
        