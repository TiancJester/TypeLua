
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

    // <last statement> ::= break ';'
    public class Laststatement_Break_Semi : Last_statement_basisproduction
    {
        public Token<string> Break;
        public Token<string> Semi;

        public Laststatement_Break_Semi(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Break = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Break);
            this.Semi = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Semi);
        }

        public override void ContextVerify(IContext context)
        {
            var loopContext = context.GetParentContextWithUntil<LoopBlockContext,Function>();
            if (loopContext == null)
            {
                throw new SyntaxException("The 'break' only can be used in loop block.", this.Break.Line, this.Break.Column);
            }
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append(depth.GetIndentation());
            builder.AppendLine("break");
        }
    }
}
        