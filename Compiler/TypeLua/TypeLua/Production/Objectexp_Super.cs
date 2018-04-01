
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <object exp> ::= super
    public class Objectexp_Super : Object_exp_basisproduction
    {
        public Token<string> Super;

        public Objectexp_Super(Project project, Class @class, GOLD.Token token0)
        {
            this.Super = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Super);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.GetExpressions(new Expression() {Classify = ExpressionType.Super});
        }

        public override void ContextVerify(IContext context)
        {
            if (context.ClassContext.BaseClass == null)
            {
                throw new SyntaxException("There is no super class.", this.Super.Line, this.Super.Column);
            }
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append("self.super");
        }
    }
}
        