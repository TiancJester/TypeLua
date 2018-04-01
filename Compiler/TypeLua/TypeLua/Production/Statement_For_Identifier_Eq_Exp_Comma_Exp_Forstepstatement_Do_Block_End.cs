
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Element;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Generate;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <statement> ::= for Identifier '=' <exp> ',' <exp> <for step statement> do <block> end
    public class Statement_For_Identifier_Eq_Exp_Comma_Exp_Forstepstatement_Do_Block_End : Statement_basisproduction
    {
        public Token<string> For;
        public Token<string> Identifier;
        public Token<string> Eq;
        public Token<Exp_basisproduction> Exp;
        public Token<string> Comma;
        public Token<Exp_basisproduction> Exp_2;
        public Token<For_step_statement_basisproduction> Forstepstatement;
        public Token<string> Do;
        public Token<Block_basisproduction> Block;
        public Token<string> End;

        public Statement_For_Identifier_Eq_Exp_Comma_Exp_Forstepstatement_Do_Block_End(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2,GOLD.Token token3,GOLD.Token token4,GOLD.Token token5,GOLD.Token token6,GOLD.Token token7,GOLD.Token token8,GOLD.Token token9)
        {
            this.For = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.For);
            this.Identifier = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Identifier);
            this.Eq = new Token<string>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (string)token2.Data};
            this.Children.Add(this.Eq);
            this.Exp = new Token<Exp_basisproduction>() {Column = token3.Position().Column,Line = token3 .Position().Line,Symbol = (Exp_basisproduction)token3.Data};
            this.Children.Add(this.Exp);
            this.Comma = new Token<string>() {Column = token4.Position().Column,Line = token4 .Position().Line,Symbol = (string)token4.Data};
            this.Children.Add(this.Comma);
            this.Exp_2 = new Token<Exp_basisproduction>() {Column = token5.Position().Column,Line = token5 .Position().Line,Symbol = (Exp_basisproduction)token5.Data};
            this.Children.Add(this.Exp_2);
            this.Forstepstatement = new Token<For_step_statement_basisproduction>() {Column = token6.Position().Column,Line = token6 .Position().Line,Symbol = (For_step_statement_basisproduction)token6.Data};
            this.Children.Add(this.Forstepstatement);
            this.Do = new Token<string>() {Column = token7.Position().Column,Line = token7 .Position().Line,Symbol = (string)token7.Data};
            this.Children.Add(this.Do);
            this.Block = new Token<Block_basisproduction>() {Column = token8.Position().Column,Line = token8 .Position().Line,Symbol = (Block_basisproduction)token8.Data};
            this.Children.Add(this.Block);
            this.End = new Token<string>() {Column = token9.Position().Column,Line = token9 .Position().Line,Symbol = (string)token9.Data};
            this.Children.Add(this.End);
        }

        public override void ContextVerify(IContext context)
        {
            //重复变量检查
            var v1 = context.GetElement(this.Identifier.Symbol, context);
            if (v1 != null)
            {
                throw new SyntaxException("Variable with same name is already exist", this.Identifier.Line, this.Identifier.Column);
            }

            var expValueList1 = this.Exp.Symbol.GetExpressions(context.ClassContext.Packages, context);
            if (expValueList1.Length != 1)
            {
                throw new SyntaxException("Expression mismatch.", this.Eq.Line, this.Eq.Column);
            }
            var expValue1 = expValueList1[0];
            if (expValue1.Classify != ExpressionType.Value || expValue1.Type != Type.Number)
            {
                throw new SyntaxException("The expression must be a number.", this.Eq.Line, this.Eq.Column);
            }
            this.Exp.Symbol.ContextVerify(context);

            if (this.Exp_2 != null)
            {
                var expValueList2 = this.Exp_2.Symbol.GetExpressions(context.ClassContext.Packages, context);
                if (expValueList2.Length != 1)
                {
                    throw new SyntaxException("Expression mismatch.", this.Comma.Line, this.Comma.Column);
                }
                var expValue2 = expValueList2[0];
                if (expValue2.Classify != ExpressionType.Value || expValue2.Type != Type.Number)
                {
                    throw new SyntaxException("The expression must be a number.", this.Eq.Line, this.Eq.Column);
                }
                this.Exp_2.Symbol.ContextVerify(context);
            }

            var forContext = new LoopBlockContext();
            forContext.ParentContext = context;
            forContext.ClassContext = context.ClassContext;
            forContext.AddElement(new Variable(this.Identifier.Symbol) { Type = Type.Number });

            this.Forstepstatement.Symbol.ContextVerify(forContext);
            this.Block.Symbol.ContextVerify(forContext);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append(depth.GetIndentation());
            builder.Append(string.Format("for {0} = ", this.Identifier.Symbol));
            this.Exp.Symbol.GenerateLua(c, root, builder, depth);
            builder.Append(", ");
            this.Exp_2.Symbol.GenerateLua(c, root, builder, depth);
            this.Forstepstatement.Symbol.GenerateLua(c, root, builder, depth);
            builder.AppendLine(" do");

            Block.Symbol.GenerateLua(c, root, builder, depth + 1);

            builder.Append(depth.GetIndentation());
            builder.AppendLine("end");
        }
    }
}
        