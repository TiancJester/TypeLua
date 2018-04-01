
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <add exp> ::= <mult exp>
    public class Addexp_Multexp : Add_exp_basisproduction
    {
        public Token<Mult_exp_basisproduction> Multexp;

        public Addexp_Multexp(Project project, Class @class, GOLD.Token token0)
        {
            this.Multexp = new Token<Mult_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Mult_exp_basisproduction)token0.Data};
            this.Children.Add(this.Multexp);
        }

        public override Exp_basisproduction GetFinalExp()
        {
            return this.Multexp.Symbol.GetFinalExp();
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.Multexp.Symbol.GetExpressions(packagesContext,expContext);
        }

        public override void ContextVerify(IContext context)
        {
            this.Multexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Multexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        