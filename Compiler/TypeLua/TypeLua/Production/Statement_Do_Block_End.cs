
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Generate;
    using TypeLua.Project.Types;

    // <statement> ::= do <block> end
    public class Statement_Do_Block_End : Statement_basisproduction
    {
        public Token<string> Do;
        public Token<Block_basisproduction> Block;
        public Token<string> End;

        public Statement_Do_Block_End(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2)
        {
            this.Do = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Do);
            this.Block = new Token<Block_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Block_basisproduction)token1.Data};
            this.Children.Add(this.Block);
            this.End = new Token<string>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (string)token2.Data};
            this.Children.Add(this.End);
        }

        public override void ContextVerify(IContext context)
        {
            var doEndBlock = new Context();
            doEndBlock.ParentContext = context;
            doEndBlock.ClassContext = context.ClassContext;
            this.Block.Symbol.ContextVerify(doEndBlock);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append(depth.GetIndentation());
            builder.AppendLine("do");

            Block.Symbol.GenerateLua(c, root, builder, depth + 1);

            builder.Append(depth.GetIndentation());
            builder.AppendLine("end");
        }
    }
}
        