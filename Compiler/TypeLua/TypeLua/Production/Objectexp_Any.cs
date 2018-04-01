
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <object exp> ::= any
    public class Objectexp_Any : Object_exp_basisproduction
    {
        public Token<string> Any;

        public Objectexp_Any(Project project, Class @class, GOLD.Token token0)
        {
            this.Any = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Any);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.GetExpressionsWithClass(Type.Any);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
        }
    }
}
        