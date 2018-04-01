
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <or exp> ::= <or exp> or <and exp>
    public class Orexp_Orexp_Or_Andexp : Or_exp_basisproduction
    {
        public Token<Or_exp_basisproduction> Orexp;
        public Token<string> Or;
        public Token<And_exp_basisproduction> Andexp;

        public Orexp_Orexp_Or_Andexp(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2)
        {
            this.Orexp = new Token<Or_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Or_exp_basisproduction)token0.Data};
            this.Children.Add(this.Orexp);
            this.Or = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Or);
            this.Andexp = new Token<And_exp_basisproduction>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (And_exp_basisproduction)token2.Data};
            this.Children.Add(this.Andexp);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.GetExpressionsWithValue(Type.Bool);
        }

        public override void ContextVerify(IContext context)
        {
            this.Orexp.Symbol.ContextVerify(context);
            this.Andexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Orexp.Symbol.GenerateLua(c, root, builder, depth);
            builder.Append(" or ");
            Andexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        