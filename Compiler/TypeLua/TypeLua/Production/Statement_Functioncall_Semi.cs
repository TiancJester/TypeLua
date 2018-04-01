
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Generate;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <statement> ::= <function call> ';'
    public class Statement_Functioncall_Semi : Statement_basisproduction
    {
        public Token<Function_call_basisproduction> Functioncall;
        public Token<string> Semi;

        public Statement_Functioncall_Semi(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Functioncall = new Token<Function_call_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Function_call_basisproduction)token0.Data};
            this.Children.Add(this.Functioncall);
            this.Semi = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Semi);
        }

        public override void ContextVerify(IContext context)
        {
            this.Functioncall.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append(depth.GetIndentation());
            this.Functioncall.Symbol.GenerateLua(c, root, builder, depth);
            builder.AppendLine();
        }
    }
}
        