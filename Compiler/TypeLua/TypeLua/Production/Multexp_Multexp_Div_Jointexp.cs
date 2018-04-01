
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <mult exp> ::= <mult exp> '/' <joint exp>
    public class Multexp_Multexp_Div_Jointexp : Mult_exp_basisproduction
    {
        public Token<Mult_exp_basisproduction> Multexp;
        public Token<string> Div;
        public Token<Joint_exp_basisproduction> Jointexp;

        public Multexp_Multexp_Div_Jointexp(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2)
        {
            this.Multexp = new Token<Mult_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Mult_exp_basisproduction)token0.Data};
            this.Children.Add(this.Multexp);
            this.Div = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Div);
            this.Jointexp = new Token<Joint_exp_basisproduction>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (Joint_exp_basisproduction)token2.Data};
            this.Children.Add(this.Jointexp);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.GetExpressionsWithValue(Type.Number);
        }

        public override void ContextVerify(IContext context)
        {
            if (!this.Multexp.Symbol.ExpTypeIs(Type.Number, context.ClassContext.Packages, context) || !this.Jointexp.Symbol.ExpTypeIs(Type.Number, context.ClassContext.Packages, context))
            {
                throw new SyntaxException("Cannot apply operator '/' here.", this.Div.Line, this.Div.Column);
            }

            this.Multexp.Symbol.ContextVerify(context);
            this.Jointexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Multexp.Symbol.GenerateLua(c, root, builder, depth);
            builder.Append(" / ");
            Jointexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        