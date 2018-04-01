
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <equality exp> ::= <compare exp>
    public class Equalityexp_Compareexp : Equality_exp_basisproduction
    {
        public Token<Compare_exp_basisproduction> Compareexp;

        public Equalityexp_Compareexp(Project project, Class @class, GOLD.Token token0)
        {
            this.Compareexp = new Token<Compare_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Compare_exp_basisproduction)token0.Data};
            this.Children.Add(this.Compareexp);
        }

        public override Exp_basisproduction GetFinalExp()
        {
            return this.Compareexp.Symbol.GetFinalExp();
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return Compareexp.Symbol.GetExpressions(packagesContext,expContext);
        }

        public override void ContextVerify(IContext context)
        {
            this.Compareexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Compareexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        