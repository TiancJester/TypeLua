
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <unary exp> ::= '#' <unary exp>
    public class Unaryexp_Num_Unaryexp : Unary_exp_basisproduction
    {
        public Token<string> Num;
        public Token<Unary_exp_basisproduction> Unaryexp;

        public Unaryexp_Num_Unaryexp(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Num = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Num);
            this.Unaryexp = new Token<Unary_exp_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Unary_exp_basisproduction)token1.Data};
            this.Children.Add(this.Unaryexp);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.GetExpressionsWithValue(Type.Number);
        }

        public override void ContextVerify(IContext context)
        {
            var tlValues = this.Unaryexp.Symbol.GetExpressions(context.ClassContext.Packages, context);
            if (tlValues.Length == 1 && tlValues[0].Type.Name == Type.ListTable.Name
                && tlValues[0].Type.PackageName == Type.ListTable.PackageName)
            {
                this.Unaryexp.Symbol.ContextVerify(context);
            }
            else
            {
                throw new SyntaxException("Cannot apply operator '#' here.", this.Num.Line, this.Num.Column);
            }
        }
        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append("#");
            Unaryexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        