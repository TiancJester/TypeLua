
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <function exp> ::= <function body>
    public class Functionexp_Functionbody : Function_exp_basisproduction
    {
        public Token<Function_body_basisproduction> Functionbody;

        public Functionexp_Functionbody(Project project, Class @class, GOLD.Token token0)
        {
            this.Functionbody = new Token<Function_body_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Function_body_basisproduction)token0.Data};
            this.Children.Add(this.Functionbody);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            var functionContext = this.Functionbody.Symbol.GetFunctionContext(expContext);

            return this.GetExpressions(new Expression() { Classify = ExpressionType.Value, Type = functionContext.Type, Element = functionContext });
        }

        public override void ContextVerify(IContext context)
        {
            this.Functionbody.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Functionbody.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        