
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <unary exp> ::= not <unary exp>
    public class Unaryexp_Not_Unaryexp : Unary_exp_basisproduction
    {
        public Token<string> Not;
        public Token<Unary_exp_basisproduction> Unaryexp;

        public Unaryexp_Not_Unaryexp(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Not = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Not);
            this.Unaryexp = new Token<Unary_exp_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Unary_exp_basisproduction)token1.Data};
            this.Children.Add(this.Unaryexp);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.GetExpressionsWithValue(Type.Bool);
        }

        public override void ContextVerify(IContext context)
        {
            this.Unaryexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append("not ");
            Unaryexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        