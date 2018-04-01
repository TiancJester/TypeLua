
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <type conversion exp> ::= <add exp>
    public class Typeconversionexp_Addexp : Type_conversion_exp_basisproduction
    {
        public Token<Add_exp_basisproduction> Addexp;

        public Typeconversionexp_Addexp(Project project, Class @class, GOLD.Token token0)
        {
            this.Addexp = new Token<Add_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Add_exp_basisproduction)token0.Data};
            this.Children.Add(this.Addexp);
        }

        public override Exp_basisproduction GetFinalExp()
        {
            return this.Addexp.Symbol.GetFinalExp();
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.Addexp.Symbol.GetExpressions(packagesContext,expContext);
        }

        public override void ContextVerify(IContext context)
        {
            this.Addexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Addexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        