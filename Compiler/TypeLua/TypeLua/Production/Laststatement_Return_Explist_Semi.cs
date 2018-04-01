
namespace TypeLua.Production
{
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Element;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Generate;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <last statement> ::= return <exp list> ';'
    public class Laststatement_Return_Explist_Semi : Last_statement_basisproduction
    {
        public Token<string> Return;
        public Token<Exp_list_basisproduction> Explist;
        public Token<string> Semi;

        public Laststatement_Return_Explist_Semi(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2)
        {
            this.Return = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Return);
            this.Explist = new Token<Exp_list_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Exp_list_basisproduction)token1.Data};
            this.Children.Add(this.Explist);
            this.Semi = new Token<string>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (string)token2.Data};
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

            var returnExps = this.Explist.Symbol.GetExps(new List<Token<Exp_basisproduction>>());
            List<Expression> returnValues = new List<Expression>();
            foreach (var returnExp in returnExps)
            {
                var tlValues = returnExp.Symbol.GetExpressions(context.ClassContext.Packages,context);
                returnValues.AddRange(tlValues);
                returnExp.Symbol.ContextVerify(context);
            }

            if (returnValueTypes.Length != returnValues.Count)
            {
                throw new SyntaxException(
                        "The return count not match.",
                        this.Return.Line,
                        this.Return.Column);
            }

            for (int i = 0; i < returnValues.Count; i++)
            {
                var realType = returnValues[i];
                var wantType = returnValueTypes[i];
                if (!wantType.IsAssignableFrom(realType.Type))
                {
                    throw new SyntaxException(
                        string.Format("The return value not match. Cannot convert from '{0}' to '{1}'.", realType.Type.FullName,wantType.FullName),
                        this.Return.Line,
                        this.Return.Column);
                }
            }
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append(depth.GetIndentation());
            builder.Append("return ");
            this.Explist.Symbol.GenerateLua(c, root, builder, depth);
            builder.AppendLine();
        }
    }
}
        