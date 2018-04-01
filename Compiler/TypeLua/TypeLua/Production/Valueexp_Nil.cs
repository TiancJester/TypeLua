
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <value exp> ::= nil
    public class Valueexp_Nil : Value_exp_basisproduction
    {
        public Token<string> Nil;

        public Valueexp_Nil(Project project, Class @class, GOLD.Token token0)
        {
            this.Nil = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Nil);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.GetExpressionsWithValue(Type.Nil);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append("nil");
        }
    }
}
        