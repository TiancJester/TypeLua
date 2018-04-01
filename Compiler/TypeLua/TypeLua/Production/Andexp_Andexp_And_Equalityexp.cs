
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <and exp> ::= <and exp> and <equality exp>
    public class Andexp_Andexp_And_Equalityexp : And_exp_basisproduction
    {
        public Token<And_exp_basisproduction> Andexp;
        public Token<string> And;
        public Token<Equality_exp_basisproduction> Equalityexp;

        public Andexp_Andexp_And_Equalityexp(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2)
        {
            this.Andexp = new Token<And_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (And_exp_basisproduction)token0.Data};
            this.Children.Add(this.Andexp);
            this.And = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.And);
            this.Equalityexp = new Token<Equality_exp_basisproduction>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (Equality_exp_basisproduction)token2.Data};
            this.Children.Add(this.Equalityexp);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.GetExpressionsWithValue(Type.Bool);
        }

        public override void ContextVerify(IContext context)
        {
            this.Andexp.Symbol.ContextVerify(context);
            this.Equalityexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Andexp.Symbol.GenerateLua(c, root, builder, depth);
            builder.Append(" and ");
            Equalityexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}