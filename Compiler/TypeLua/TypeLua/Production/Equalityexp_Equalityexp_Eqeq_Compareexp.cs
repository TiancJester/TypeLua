
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <equality exp> ::= <equality exp> '==' <compare exp>
    public class Equalityexp_Equalityexp_Eqeq_Compareexp : Equality_exp_basisproduction
    {
        public Token<Equality_exp_basisproduction> Equalityexp;
        public Token<string> Eqeq;
        public Token<Compare_exp_basisproduction> Compareexp;

        public Equalityexp_Equalityexp_Eqeq_Compareexp(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2)
        {
            this.Equalityexp = new Token<Equality_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Equality_exp_basisproduction)token0.Data};
            this.Children.Add(this.Equalityexp);
            this.Eqeq = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Eqeq);
            this.Compareexp = new Token<Compare_exp_basisproduction>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (Compare_exp_basisproduction)token2.Data};
            this.Children.Add(this.Compareexp);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            this.Equalityexp.Symbol.GetExpressions(packagesContext, expContext);
            this.Compareexp.Symbol.GetExpressions(packagesContext, expContext);
            return this.GetExpressionsWithValue(Type.Bool);
        }

        public override void ContextVerify(IContext context)
        {
            this.Equalityexp.Symbol.ContextVerify(context);
            this.Compareexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Equalityexp.Symbol.GenerateLua(c, root, builder, depth);
            builder.Append(" == ");
            Compareexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        