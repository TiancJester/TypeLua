
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <joint exp> ::= <joint exp> '..' <unary exp>
    public class Jointexp_Jointexp_Dotdot_Unaryexp : Joint_exp_basisproduction
    {
        public Token<Joint_exp_basisproduction> Jointexp;
        public Token<string> Dotdot;
        public Token<Unary_exp_basisproduction> Unaryexp;

        public Jointexp_Jointexp_Dotdot_Unaryexp(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2)
        {
            this.Jointexp = new Token<Joint_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Joint_exp_basisproduction)token0.Data};
            this.Children.Add(this.Jointexp);
            this.Dotdot = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Dotdot);
            this.Unaryexp = new Token<Unary_exp_basisproduction>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (Unary_exp_basisproduction)token2.Data};
            this.Children.Add(this.Unaryexp);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.GetExpressionsWithValue(Type.String);
        }

        public override void ContextVerify(IContext context)
        {
            this.Jointexp.Symbol.ContextVerify(context);
            this.Unaryexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Jointexp.Symbol.GenerateLua(c, root, builder, depth);
            builder.Append(" .. ");
            Unaryexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        