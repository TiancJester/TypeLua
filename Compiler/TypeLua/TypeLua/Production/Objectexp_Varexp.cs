
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <object exp> ::= <var exp>
    public class Objectexp_Varexp : Object_exp_basisproduction
    {
        public Token<Var_exp_basisproduction> Varexp;

        public Objectexp_Varexp(Project project, Class @class, GOLD.Token token0)
        {
            this.Varexp = new Token<Var_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Var_exp_basisproduction)token0.Data};
            this.Children.Add(this.Varexp);
        }

        public override Exp_basisproduction GetFinalExp()
        {
            return this.Varexp.Symbol.GetFinalExp();
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.Varexp.Symbol.GetExpressions(packagesContext,expContext);
        }

        public override void ContextVerify(IContext context)
        {
            this.Varexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Varexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        