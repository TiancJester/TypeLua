
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <object exp> ::= this
    public class Objectexp_This : Object_exp_basisproduction
    {
        public Token<string> This;

        public Objectexp_This(Project project, Class @class, GOLD.Token token0)
        {
            this.This = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.This);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.GetExpressions(new Expression() { Classify = ExpressionType.This,Type = expContext.ClassContext.GetTLType()});
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append("self");
        }
    }
}
        