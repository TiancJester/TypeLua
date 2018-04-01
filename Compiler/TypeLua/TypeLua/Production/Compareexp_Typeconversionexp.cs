
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <compare exp> ::= <type conversion exp>
    public class Compareexp_Typeconversionexp : Compare_exp_basisproduction
    {
        public Token<Type_conversion_exp_basisproduction> Typeconversionexp;

        public Compareexp_Typeconversionexp(Project project, Class @class, GOLD.Token token0)
        {
            this.Typeconversionexp = new Token<Type_conversion_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Type_conversion_exp_basisproduction)token0.Data};
            this.Children.Add(this.Typeconversionexp);
        }

        public override Exp_basisproduction GetFinalExp()
        {
            return this.Typeconversionexp.Symbol.GetFinalExp();
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.Typeconversionexp.Symbol.GetExpressions(packagesContext,expContext);
        }

        public override void ContextVerify(IContext context)
        {
            this.Typeconversionexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Typeconversionexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        