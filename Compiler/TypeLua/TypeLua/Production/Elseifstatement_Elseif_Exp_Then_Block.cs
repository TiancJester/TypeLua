
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Generate;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <else if statement> ::= elseif <exp> then <block>
    public class Elseifstatement_Elseif_Exp_Then_Block : Else_if_statement_basisproduction
    {
        public Token<string> Elseif;
        public Token<Exp_basisproduction> Exp;
        public Token<string> Then;
        public Token<Block_basisproduction> Block;

        public Elseifstatement_Elseif_Exp_Then_Block(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2,GOLD.Token token3)
        {
            this.Elseif = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Elseif);
            this.Exp = new Token<Exp_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Exp_basisproduction)token1.Data};
            this.Children.Add(this.Exp);
            this.Then = new Token<string>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (string)token2.Data};
            this.Children.Add(this.Then);
            this.Block = new Token<Block_basisproduction>() {Column = token3.Position().Column,Line = token3 .Position().Line,Symbol = (Block_basisproduction)token3.Data};
            this.Children.Add(this.Block);
        }

        public override void ContextVerify(IContext context)
        {
            var conditionExpValue = this.Exp.Symbol.GetExpressions(context.ClassContext.Packages, context);
            if (conditionExpValue.Length > 1)
            {
                throw new SyntaxException("Cannot use multi-value in condition expression.", this.Elseif.Line, this.Elseif.Column);
            }
            if (conditionExpValue.Length == 0)
            {
                throw new SyntaxException("Cannot use non-value in condition expression.", this.Elseif.Line, this.Elseif.Column);
            }
            var tlValue = conditionExpValue[0];
            if (tlValue.Classify != ExpressionType.Value)
            {
                throw new SyntaxException("Condition expression is not a value.", this.Elseif.Line, this.Elseif.Column);
            }
            this.Exp.Symbol.ContextVerify(context);

            var thenContext = new Context();
            thenContext.ClassContext = context.ClassContext;
            thenContext.ParentContext = context;

            this.Block.Symbol.ContextVerify(thenContext);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append(depth.GetIndentation());
            builder.Append("elseif ");
            Exp.Symbol.GenerateLua(c,root,builder,depth);
            builder.AppendLine(" then");
            Block.Symbol.GenerateLua(c, root, builder, depth + 1);
        }
    }
}
        