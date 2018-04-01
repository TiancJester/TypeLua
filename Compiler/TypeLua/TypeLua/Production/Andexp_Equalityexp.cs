
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <and exp> ::= <equality exp>
    public class Andexp_Equalityexp : And_exp_basisproduction
    {
        public Token<Equality_exp_basisproduction> Equalityexp;

        public Andexp_Equalityexp(Project project, Class @class, GOLD.Token token0)
        {
            this.Equalityexp = new Token<Equality_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Equality_exp_basisproduction)token0.Data};
            this.Children.Add(this.Equalityexp);
        }

        public override Exp_basisproduction GetFinalExp()
        {
            return this.Equalityexp.Symbol.GetFinalExp();
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.Equalityexp.Symbol.GetExpressions(packagesContext,expContext);
        }

        public override void ContextVerify(IContext context)
        {
            this.Equalityexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Equalityexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        