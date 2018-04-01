
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <value exp> ::= <object exp>
    public class Valueexp_Objectexp : Value_exp_basisproduction
    {
        public Token<Object_exp_basisproduction> Objectexp;

        public Valueexp_Objectexp(Project project, Class @class, GOLD.Token token0)
        {
            this.Objectexp = new Token<Object_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Object_exp_basisproduction)token0.Data};
            this.Children.Add(this.Objectexp);
        }

        public override Exp_basisproduction GetFinalExp()
        {
            return this.Objectexp.Symbol.GetFinalExp();
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.Objectexp.Symbol.GetExpressions(packagesContext,expContext);
        }

        public override void ContextVerify(IContext context)
        {
            this.Objectexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            this.Objectexp.Symbol.GenerateLua(c,root,builder,depth);
        }
    }
}
        