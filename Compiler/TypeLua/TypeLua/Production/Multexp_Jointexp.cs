
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <mult exp> ::= <joint exp>
    public class Multexp_Jointexp : Mult_exp_basisproduction
    {
        public Token<Joint_exp_basisproduction> Jointexp;

        public Multexp_Jointexp(Project project, Class @class, GOLD.Token token0)
        {
            this.Jointexp = new Token<Joint_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Joint_exp_basisproduction)token0.Data};
            this.Children.Add(this.Jointexp);
        }

        public override Exp_basisproduction GetFinalExp()
        {
            return this.Jointexp.Symbol.GetFinalExp();
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.Jointexp.Symbol.GetExpressions(packagesContext,expContext);
        }

        public override void ContextVerify(IContext context)
        {
            this.Jointexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Jointexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        