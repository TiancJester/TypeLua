
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <function exp> ::= <value exp>
    public class Functionexp_Valueexp : Function_exp_basisproduction
    {
        public Token<Value_exp_basisproduction> Valueexp;

        public Functionexp_Valueexp(Project project, Class @class, GOLD.Token token0)
        {
            this.Valueexp = new Token<Value_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Value_exp_basisproduction)token0.Data};
            this.Children.Add(this.Valueexp);
        }

        public override Exp_basisproduction GetFinalExp()
        {
            return this.Valueexp.Symbol.GetFinalExp();
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.Valueexp.Symbol.GetExpressions(packagesContext,expContext);
        }

        public override void ContextVerify(IContext context)
        {
            this.Valueexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Valueexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        