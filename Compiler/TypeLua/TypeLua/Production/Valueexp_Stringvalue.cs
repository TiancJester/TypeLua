
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <value exp> ::= StringValue
    public class Valueexp_Stringvalue : Value_exp_basisproduction
    {
        public Token<string> Stringvalue;

        public Valueexp_Stringvalue(Project project, Class @class, GOLD.Token token0)
        {
            this.Stringvalue = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Stringvalue);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.GetExpressionsWithValue(Type.String);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append(this.Stringvalue.Symbol);
        }
    }
}
        