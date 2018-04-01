
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <for step statement> ::= ',' <exp>
    public class Forstepstatement_Comma_Exp : For_step_statement_basisproduction
    {
        public Token<string> Comma;
        public Token<Exp_basisproduction> Exp;

        public Forstepstatement_Comma_Exp(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Comma = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Comma);
            this.Exp = new Token<Exp_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Exp_basisproduction)token1.Data};
            this.Children.Add(this.Exp);
        }

        public override void ContextVerify(IContext context)
        {
            var expValueList = this.Exp.Symbol.GetExpressions(context.ClassContext.Packages, context);
            if (expValueList.Length != 1)
            {
                throw new SyntaxException("Expression mismatch.", this.Comma.Line, this.Comma.Column);
            }
            var expValue = expValueList[0];
            if (expValue.Classify != ExpressionType.Value || expValue.Type != Type.Number)
            {
                throw new SyntaxException("The expression must be a number.", this.Comma.Line, this.Comma.Column);
            }
            
            this.Exp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append(",");
            this.Exp.Symbol.GenerateLua(c,root,builder,depth);
        }
    }
}
        