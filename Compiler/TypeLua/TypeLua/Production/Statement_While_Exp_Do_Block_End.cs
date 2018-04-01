
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Generate;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <statement> ::= while <exp> do <block> end
    public class Statement_While_Exp_Do_Block_End : Statement_basisproduction
    {
        public Token<string> While;
        public Token<Exp_basisproduction> Exp;
        public Token<string> Do;
        public Token<Block_basisproduction> Block;
        public Token<string> End;

        public Statement_While_Exp_Do_Block_End(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2,GOLD.Token token3,GOLD.Token token4)
        {
            this.While = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.While);
            this.Exp = new Token<Exp_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Exp_basisproduction)token1.Data};
            this.Children.Add(this.Exp);
            this.Do = new Token<string>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (string)token2.Data};
            this.Children.Add(this.Do);
            this.Block = new Token<Block_basisproduction>() {Column = token3.Position().Column,Line = token3 .Position().Line,Symbol = (Block_basisproduction)token3.Data};
            this.Children.Add(this.Block);
            this.End = new Token<string>() {Column = token4.Position().Column,Line = token4 .Position().Line,Symbol = (string)token4.Data};
            this.Children.Add(this.End);
        }

        public override void ContextVerify(IContext context)
        {
            var conditionExpValue = this.Exp.Symbol.GetExpressions(context.ClassContext.Packages, context);
            if (conditionExpValue.Length > 1)
            {
                throw new SyntaxException("Cannot use multi-value in condition expression.", this.While.Line, this.While.Column);
            }
            if (conditionExpValue.Length == 0)
            {
                throw new SyntaxException("Cannot use non-value in condition expression.", this.While.Line, this.While.Column);
            }
            var tlValue = conditionExpValue[0];
            if (tlValue.Classify != ExpressionType.Value)
            {
                throw new SyntaxException("Condition expression is not a value.", this.While.Line, this.While.Column);
            }
            this.Exp.Symbol.ContextVerify(context);

            var whileContext = new LoopBlockContext();
            whileContext.ClassContext = context.ClassContext;
            whileContext.ParentContext = context;

            this.Block.Symbol.ContextVerify(whileContext);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append(depth.GetIndentation());
            builder.Append("while ");
            this.Exp.Symbol.GenerateLua(c, root, builder, depth);
            builder.AppendLine(" do");

            this.Block.Symbol.GenerateLua(c, root, builder, depth + 1);

            builder.Append(depth.GetIndentation());
            builder.AppendLine("end");
        }
    }
}
        