
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

    // <statement> ::= for Identifier ',' Identifier in <exp> do <block> end
    public class Statement_For_Identifier_Comma_Identifier_In_Exp_Do_Block_End : Statement_basisproduction
    {
        public Token<string> For;
        public Token<string> Identifier;
        public Token<string> Comma;
        public Token<string> Identifier_2;
        public Token<string> In;
        public Token<Exp_basisproduction> Exp;
        public Token<string> Do;
        public Token<Block_basisproduction> Block;
        public Token<string> End;

        public Statement_For_Identifier_Comma_Identifier_In_Exp_Do_Block_End(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2,GOLD.Token token3,GOLD.Token token4,GOLD.Token token5,GOLD.Token token6,GOLD.Token token7,GOLD.Token token8)
        {
            this.For = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.For);
            this.Identifier = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Identifier);
            this.Comma = new Token<string>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (string)token2.Data};
            this.Children.Add(this.Comma);
            this.Identifier_2 = new Token<string>() {Column = token3.Position().Column,Line = token3 .Position().Line,Symbol = (string)token3.Data};
            this.Children.Add(this.Identifier_2);
            this.In = new Token<string>() {Column = token4.Position().Column,Line = token4 .Position().Line,Symbol = (string)token4.Data};
            this.Children.Add(this.In);
            this.Exp = new Token<Exp_basisproduction>() {Column = token5.Position().Column,Line = token5 .Position().Line,Symbol = (Exp_basisproduction)token5.Data};
            this.Children.Add(this.Exp);
            this.Do = new Token<string>() {Column = token6.Position().Column,Line = token6 .Position().Line,Symbol = (string)token6.Data};
            this.Children.Add(this.Do);
            this.Block = new Token<Block_basisproduction>() {Column = token7.Position().Column,Line = token7 .Position().Line,Symbol = (Block_basisproduction)token7.Data};
            this.Children.Add(this.Block);
            this.End = new Token<string>() {Column = token8.Position().Column,Line = token8 .Position().Line,Symbol = (string)token8.Data};
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
            var v2 = context.GetElement(this.Identifier_2.Symbol, context);
            if (v2 != null)
            {
                throw new SyntaxException("Variable with same name is already exist", this.Identifier_2.Line, this.Identifier_2.Column);
            }

            var expValueList = this.Exp.Symbol.GetExpressions(context.ClassContext.Packages, context);
            if (expValueList.Length != 1)
            {
                throw new SyntaxException("Expression mismatch.", this.In.Line, this.In.Column);
            }
            var expValue = expValueList[0];
            if (expValue.Classify != ExpressionType.Value)
            {
                throw new SyntaxException("Expression mismatch.", this.In.Line, this.In.Column);
            }
            this.Exp.Symbol.ContextVerify(context);

            var forContext = new LoopBlockContext();
            forContext.ParentContext = context;
            forContext.ClassContext = context.ClassContext;

            if (expValue.Type == Type.Table || expValue.Type == Type.Any)
            {
                forContext.AddElement(new Variable(this.Identifier.Symbol) { Type = Type.Any });
                forContext.AddElement(new Variable(this.Identifier_2.Symbol) { Type = Type.Any });
            }
            else if (expValue.Type.Name == Type.ListTable.Name && expValue.Type.PackageName == Type.ListTable.PackageName)
            {
                var tlGenericityType = expValue.Type as GenericityType;
                forContext.AddElement(new Variable(this.Identifier.Symbol) { Type = Type.Number });
                forContext.AddElement(new Variable(this.Identifier_2.Symbol) { Type = tlGenericityType.FirstGroupGenericTypeArguments[0] });
            }
            else if (expValue.Type.Name == Type.HashTable.Name && expValue.Type.PackageName == Type.HashTable.PackageName)
            {
                var tlGenericityType = expValue.Type as GenericityType;
                forContext.AddElement(new Variable(this.Identifier.Symbol) { Type = tlGenericityType.FirstGroupGenericTypeArguments[0] });
                forContext.AddElement(new Variable(this.Identifier_2.Symbol) { Type = tlGenericityType.FirstGroupGenericTypeArguments[1] });
            }
            else
            {
                throw new SyntaxException("Expression mismatch.", this.In.Line, this.In.Column);
            }
            this.Block.Symbol.ContextVerify(forContext);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append(depth.GetIndentation());
            builder.Append(string.Format("for {0}, {1} in pairs(", this.Identifier.Symbol, this.Identifier_2.Symbol));
            this.Exp.Symbol.GenerateLua(c, root, builder, depth);
            builder.AppendLine(") do");

            Block.Symbol.GenerateLua(c, root, builder, depth + 1);

            builder.Append(depth.GetIndentation());
            builder.AppendLine("end");
        }
    }
}
        