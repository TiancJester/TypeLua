// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>05/03/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Element;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Generate;
    using TypeLua.Project.Types;

    // <last statement> ::= return ';'
    public class Laststatement_Return_Semi : Last_statement_basisproduction
    {
        public Token<string> Return;
        public Token<string> Semi;

        public Laststatement_Return_Semi(Project project, Class @class, GOLD.Token token0, GOLD.Token token1)
        {
            this.Return = new Token<string>() { Column = token0.Position().Column, Line = token0.Position().Line, Symbol = (string)token0.Data };
            this.Children.Add(this.Return);
            this.Semi = new Token<string>() { Column = token1.Position().Column, Line = token1.Position().Line, Symbol = (string)token1.Data };
            this.Children.Add(this.Semi);
        }

        public override void ContextVerify(IContext context)
        {
            var functionContext = context.GetParentContextWith<Function>();
            if (functionContext == null)
            {
                throw new SyntaxException("The 'return' only can be used in function block.", this.Return.Line, this.Return.Column);
            }
            var returnValueTypes = functionContext.ReturnValueTypes;

            if (returnValueTypes.Length != 0 && returnValueTypes[0] != Type.Void)
            {
                throw new SyntaxException(
                        "The return value not match.",
                        this.Return.Line,
                        this.Return.Column);
            }
        }
        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append(depth.GetIndentation());
            builder.AppendLine("return");
        }
    }
}