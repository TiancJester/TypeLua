
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <or exp> ::= <and exp>
    public class Orexp_Andexp : Or_exp_basisproduction
    {
        public Token<And_exp_basisproduction> Andexp;

        public Orexp_Andexp(Project project, Class @class, GOLD.Token token0)
        {
            this.Andexp = new Token<And_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (And_exp_basisproduction)token0.Data};
            this.Children.Add(this.Andexp);
        }

        public override Exp_basisproduction GetFinalExp()
        {
            return this.Andexp.Symbol.GetFinalExp();
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.Andexp.Symbol.GetExpressions(packagesContext,expContext);
        }

        public override void ContextVerify(IContext context)
        {
            this.Andexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Andexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        