
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <exp> ::= <or exp>
    public class Exp_Orexp : Exp_basisproduction
    {
        public Token<Or_exp_basisproduction> Orexp;

        public Exp_Orexp(Project project, Class @class, GOLD.Token token0)
        {
            this.Orexp = new Token<Or_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Or_exp_basisproduction)token0.Data};
            this.Children.Add(this.Orexp);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return Orexp.Symbol.GetExpressions(packagesContext,expContext);
        }

        public override void ContextVerify(IContext context)
        {
            this.Orexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Orexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        