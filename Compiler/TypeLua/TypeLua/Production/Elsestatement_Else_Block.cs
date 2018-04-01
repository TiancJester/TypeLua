
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Generate;
    using TypeLua.Project.Types;

    // <else statement> ::= else <block>
    public class Elsestatement_Else_Block : Else_statement_basisproduction
    {
        public Token<string> Else;
        public Token<Block_basisproduction> Block;

        public Elsestatement_Else_Block(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Else = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Else);
            this.Block = new Token<Block_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Block_basisproduction)token1.Data};
            this.Children.Add(this.Block);
        }

        public override void ContextVerify(IContext context)
        {
            var elseContext = new Context();
            elseContext.ClassContext = context.ClassContext;
            elseContext.ParentContext = context;

            this.Block.Symbol.ContextVerify(elseContext);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append(depth.GetIndentation());
            builder.AppendLine("else");
            this.Block.Symbol.GenerateLua(c, root, builder, depth + 1);
        }
    }
}
        