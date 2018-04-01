
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Generate;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <statement> ::= repeat <block> until '(' <exp> ')'
    public class Statement_Repeat_Block_Until_Lparen_Exp_Rparen : Statement_basisproduction
    {
        public Token<string> Repeat;
        public Token<Block_basisproduction> Block;
        public Token<string> Until;
        public Token<string> Lparen;
        public Token<Exp_basisproduction> Exp;
        public Token<string> Rparen;

        public Statement_Repeat_Block_Until_Lparen_Exp_Rparen(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2,GOLD.Token token3,GOLD.Token token4,GOLD.Token token5)
        {
            this.Repeat = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Repeat);
            this.Block = new Token<Block_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Block_basisproduction)token1.Data};
            this.Children.Add(this.Block);
            this.Until = new Token<string>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (string)token2.Data};
            this.Children.Add(this.Until);
            this.Lparen = new Token<string>() {Column = token3.Position().Column,Line = token3 .Position().Line,Symbol = (string)token3.Data};
            this.Children.Add(this.Lparen);
            this.Exp = new Token<Exp_basisproduction>() {Column = token4.Position().Column,Line = token4 .Position().Line,Symbol = (Exp_basisproduction)token4.Data};
            this.Children.Add(this.Exp);
            this.Rparen = new Token<string>() {Column = token5.Position().Column,Line = token5 .Position().Line,Symbol = (string)token5.Data};
            this.Children.Add(this.Rparen);
        }

        public override void ContextVerify(IContext context)
        {
            var repeatContext = new LoopBlockContext();
            repeatContext.ClassContext = context.ClassContext;
            repeatContext.ParentContext = context;

            this.Block.Symbol.ContextVerify(repeatContext);

            var conditionExpValue = this.Exp.Symbol.GetExpressions(context.ClassContext.Packages, context);
            if (conditionExpValue.Length > 1)
            {
                throw new SyntaxException("Cannot use multi-value in condition expression.", this.Lparen.Line, this.Lparen.Column);
            }
            if (conditionExpValue.Length == 0)
            {
                throw new SyntaxException("Cannot use non-value in condition expression.", this.Lparen.Line, this.Lparen.Column);
            }
            var tlValue = conditionExpValue[0];
            if (tlValue.Classify != ExpressionType.Value)
            {
                throw new SyntaxException("Condition expression is not a value.", this.Lparen.Line, this.Lparen.Column);
            }
            this.Exp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append(depth.GetIndentation());
            builder.AppendLine("repeat");

            this.Block.Symbol.GenerateLua(c, root, builder, depth + 1);

            builder.Append(depth.GetIndentation());
            builder.Append("until(");
            this.Exp.Symbol.GenerateLua(c, root, builder, depth);
            builder.AppendLine(")");
        }
    }
}
        