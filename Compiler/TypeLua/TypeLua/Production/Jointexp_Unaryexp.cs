
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <joint exp> ::= <unary exp>
    public class Jointexp_Unaryexp : Joint_exp_basisproduction
    {
        public Token<Unary_exp_basisproduction> Unaryexp;

        public Jointexp_Unaryexp(Project project, Class @class, GOLD.Token token0)
        {
            this.Unaryexp = new Token<Unary_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Unary_exp_basisproduction)token0.Data};
            this.Children.Add(this.Unaryexp);
        }

        public override Exp_basisproduction GetFinalExp()
        {
            return this.Unaryexp.Symbol.GetFinalExp();
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.Unaryexp.Symbol.GetExpressions(packagesContext,expContext);
        }

        public override void ContextVerify(IContext context)
        {
            this.Unaryexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Unaryexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        