
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <value exp> ::= NumberValue
    public class Valueexp_Numbervalue : Value_exp_basisproduction
    {
        public Token<string> Numbervalue;

        public Valueexp_Numbervalue(Project project, Class @class, GOLD.Token token0)
        {
            this.Numbervalue = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Numbervalue);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.GetExpressionsWithValue(Type.Number);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append(this.Numbervalue.Symbol);
        }
    }
}
        