
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <compare exp> ::= <compare exp> '>' <type conversion exp>
    public class Compareexp_Compareexp_Gt_Typeconversionexp : Compare_exp_basisproduction
    {
        public Token<Compare_exp_basisproduction> Compareexp;
        public Token<string> Gt;
        public Token<Type_conversion_exp_basisproduction> Typeconversionexp;

        public Compareexp_Compareexp_Gt_Typeconversionexp(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2)
        {
            this.Compareexp = new Token<Compare_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Compare_exp_basisproduction)token0.Data};
            this.Children.Add(this.Compareexp);
            this.Gt = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Gt);
            this.Typeconversionexp = new Token<Type_conversion_exp_basisproduction>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (Type_conversion_exp_basisproduction)token2.Data};
            this.Children.Add(this.Typeconversionexp);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.GetExpressionsWithValue(Type.Bool);
        }

        public override void ContextVerify(IContext context)
        {
            if (!this.Compareexp.Symbol.ExpTypeIs(Type.Number, context.ClassContext.Packages, context) || !this.Compareexp.Symbol.ExpTypeIs(Type.Number, context.ClassContext.Packages, context))
            {
                throw new SyntaxException("Cannot apply operator '>' here.", this.Gt.Line, this.Gt.Column);
            }
            this.Compareexp.Symbol.ContextVerify(context);
            this.Typeconversionexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Compareexp.Symbol.GenerateLua(c, root, builder, depth);
            builder.Append(" > ");
            Typeconversionexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        