
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <unary exp> ::= <function exp>
    public class Unaryexp_Functionexp : Unary_exp_basisproduction
    {
        public Token<Function_exp_basisproduction> Functionexp;

        public Unaryexp_Functionexp(Project project, Class @class, GOLD.Token token0)
        {
            this.Functionexp = new Token<Function_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Function_exp_basisproduction)token0.Data};
            this.Children.Add(this.Functionexp);
        }

        public override Exp_basisproduction GetFinalExp()
        {
            return this.Functionexp.Symbol.GetFinalExp();
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.Functionexp.Symbol.GetExpressions(packagesContext,expContext);
        }

        public override void ContextVerify(IContext context)
        {
            this.Functionexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Functionexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        