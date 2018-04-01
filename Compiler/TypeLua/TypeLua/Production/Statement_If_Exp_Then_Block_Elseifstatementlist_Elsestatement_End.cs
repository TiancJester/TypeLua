
namespace TypeLua.Production
{
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Generate;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <statement> ::= if <exp> then <block> <else if statement list> <else statement> end
    public class Statement_If_Exp_Then_Block_Elseifstatementlist_Elsestatement_End : Statement_basisproduction
    {
        public Token<string> If;
        public Token<Exp_basisproduction> Exp;
        public Token<string> Then;
        public Token<Block_basisproduction> Block;
        public Token<Else_if_statement_list_basisproduction> Elseifstatementlist;
        public Token<Else_statement_basisproduction> Elsestatement;
        public Token<string> End;

        public Statement_If_Exp_Then_Block_Elseifstatementlist_Elsestatement_End(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2,GOLD.Token token3,GOLD.Token token4,GOLD.Token token5,GOLD.Token token6)
        {
            this.If = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.If);
            this.Exp = new Token<Exp_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Exp_basisproduction)token1.Data};
            this.Children.Add(this.Exp);
            this.Then = new Token<string>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (string)token2.Data};
            this.Children.Add(this.Then);
            this.Block = new Token<Block_basisproduction>() {Column = token3.Position().Column,Line = token3 .Position().Line,Symbol = (Block_basisproduction)token3.Data};
            this.Children.Add(this.Block);
            this.Elseifstatementlist = new Token<Else_if_statement_list_basisproduction>() {Column = token4.Position().Column,Line = token4 .Position().Line,Symbol = (Else_if_statement_list_basisproduction)token4.Data};
            this.Children.Add(this.Elseifstatementlist);
            this.Elsestatement = new Token<Else_statement_basisproduction>() {Column = token5.Position().Column,Line = token5 .Position().Line,Symbol = (Else_statement_basisproduction)token5.Data};
            this.Children.Add(this.Elsestatement);
            this.End = new Token<string>() {Column = token6.Position().Column,Line = token6 .Position().Line,Symbol = (string)token6.Data};
            this.Children.Add(this.End);
        }

        public override void ContextVerify(IContext context)
        {
            var conditionExpValue = this.Exp.Symbol.GetExpressions(context.ClassContext.Packages, context);
            if (conditionExpValue.Length > 1)
            {
                throw new SyntaxException("Cannot use multi-value in condition expression.", this.If.Line, this.If.Column);
            }
            if (conditionExpValue.Length == 0)
            {
                throw new SyntaxException("Cannot use non-value in condition expression.", this.If.Line, this.If.Column);
            }
            var tlValue = conditionExpValue[0];
            if (tlValue.Classify != ExpressionType.Value)
            {
                throw new SyntaxException("Condition expression is not a value.", this.If.Line, this.If.Column);
            }
            this.Exp.Symbol.ContextVerify(context);

            var thenContext = new Context();
            thenContext.ClassContext = context.ClassContext;
            thenContext.ParentContext = context;

            this.Block.Symbol.ContextVerify(thenContext);

            var elseifs = this.Elseifstatementlist.Symbol.GetElseifs(new List<Token<Else_if_statement_basisproduction>>());
            foreach (var elseif in elseifs)
            {
                elseif.Symbol.ContextVerify(context);
            }

            this.Elsestatement.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append(depth.GetIndentation());
            builder.Append("if ");
            this.Exp.Symbol.GenerateLua(c, root, builder, depth);
            builder.AppendLine(" then");
            this.Block.Symbol.GenerateLua(c, root, builder, depth + 1);
            this.Elseifstatementlist.Symbol.GenerateLua(c, root, builder, depth);
            this.Elsestatement.Symbol.GenerateLua(c, root, builder, depth);

            builder.Append(depth.GetIndentation());
            builder.AppendLine("end");
        }
    }
}
        