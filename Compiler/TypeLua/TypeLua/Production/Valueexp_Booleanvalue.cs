
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <value exp> ::= BooleanValue
    public class Valueexp_Booleanvalue : Value_exp_basisproduction
    {
        public Token<string> Booleanvalue;

        public Valueexp_Booleanvalue(Project project, Class @class, GOLD.Token token0)
        {
            this.Booleanvalue = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Booleanvalue);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.GetExpressionsWithValue(Type.Bool);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append(Booleanvalue.Symbol);
        }
    }
}
        