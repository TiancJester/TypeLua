
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Types;

    // <emptyable type> ::= void
    public class Emptyabletype_Void : Emptyable_type_basisproduction
    {
        public Token<string> Void;

        public Emptyabletype_Void(Project project, Class @class, GOLD.Token token0)
        {
            this.Void = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Void);
        }

        public override bool IsEmptyType()
        {
            return true;
        }

        public override Type[] GetTLType(PackagesContext packagesContext)
        {
            return new[] { Type.Void };
        }
    }
}
        