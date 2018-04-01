
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <add exp> ::= <add exp> '-' <mult exp>
    public class Addexp_Addexp_Minus_Multexp : Add_exp_basisproduction
    {
        public Token<Add_exp_basisproduction> Addexp;
        public Token<string> Minus;
        public Token<Mult_exp_basisproduction> Multexp;

        public Addexp_Addexp_Minus_Multexp(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2)
        {
            this.Addexp = new Token<Add_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Add_exp_basisproduction)token0.Data};
            this.Children.Add(this.Addexp);
            this.Minus = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Minus);
            this.Multexp = new Token<Mult_exp_basisproduction>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (Mult_exp_basisproduction)token2.Data};
            this.Children.Add(this.Multexp);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.GetExpressionsWithValue(Type.Number);
        }

        public override void ContextVerify(IContext context)
        {
            if (!this.Addexp.Symbol.ExpTypeIs(Type.Number, context.ClassContext.Packages, context) || !this.Multexp.Symbol.ExpTypeIs(Type.Number, context.ClassContext.Packages, context))
            {
                throw new SyntaxException("Cannot apply operator '-' here.", this.Minus.Line, this.Minus.Column);
            }
            this.Addexp.Symbol.ContextVerify(context);
            this.Multexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Addexp.Symbol.GenerateLua(c, root, builder, depth);
            builder.Append(" - ");
            Multexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        