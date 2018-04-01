
namespace TypeLua.Production
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Element;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    using Type = TypeLua.Project.Types.Type;

    // <object exp> ::= new Identifier '(' <argument list> ')'
    public class Objectexp_New_Identifier_Lparen_Argumentlist_Rparen : Object_exp_basisproduction
    {
        public Token<string> New;
        public Token<string> Identifier;
        public Token<string> Lparen;
        public Token<Argument_list_basisproduction> Argumentlist;
        public Token<string> Rparen;

        public Objectexp_New_Identifier_Lparen_Argumentlist_Rparen(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2,GOLD.Token token3,GOLD.Token token4)
        {
            this.New = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.New);
            this.Identifier = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Identifier);
            this.Lparen = new Token<string>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (string)token2.Data};
            this.Children.Add(this.Lparen);
            this.Argumentlist = new Token<Argument_list_basisproduction>() {Column = token3.Position().Column,Line = token3 .Position().Line,Symbol = (Argument_list_basisproduction)token3.Data};
            this.Children.Add(this.Argumentlist);
            this.Rparen = new Token<string>() {Column = token4.Position().Column,Line = token4 .Position().Line,Symbol = (string)token4.Data};
            this.Children.Add(this.Rparen);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            var c = packagesContext.GetClass(this.Identifier.Symbol);
            if (c == null)
            {
                throw new SyntaxException(string.Format("Cannot resolve symbol {0}", this.Identifier.Symbol), this.Identifier.Line, this.Identifier.Column);
            }
            return this.GetExpressionsWithValue(packagesContext.GetTLType(this.Identifier.Symbol));
        }

        public override void ContextVerify(IContext context)
        {
            //形参
            var c = context.ClassContext.Packages.GetClass(this.Identifier.Symbol);
            if (c == null)
            {
                throw new SyntaxException(string.Format("Cannot resolve symbol {0}", this.Identifier.Symbol), this.Identifier.Line, this.Identifier.Column);
            }

            Parameter[] argumentTypes = null;
            int argumentCount = 0;
            if (c.ClassConstructor != null && c.ClassConstructor.Parameters != null)
            {
                argumentTypes = c.ClassConstructor.Parameters;
                argumentCount = argumentTypes.Length;
            }
            else
            {
                argumentCount = 0;
            }

            //实参
            var paramExpList = this.Argumentlist.Symbol.GetArguments(new List<Token<Exp_basisproduction>>());
            List<Expression> paramList = new List<Expression>();
            for (int i = 0; i < paramExpList.Count; i++)
            {
                var paramExp = paramExpList[i];
                var paramValue = paramExp.Symbol.GetExpressions(context.ClassContext.Packages, context);

                if (paramValue.Length == 0 || (paramValue.Length == 1 && paramValue[0].Type == Type.Void))
                {
                    throw new SyntaxException(string.Format("Argument {0}:Cannot use non-value expression here.", i + 1), Lparen.Line, Lparen.Column);
                }
                paramList.AddRange(paramValue);

                paramExp.Symbol.ContextVerify(context);
            }

            //数量判断
            if (argumentCount != paramList.Count)
            {
                throw new SyntaxException(string.Format("The function has {0} parameter but is invoked with {1}", argumentCount, paramExpList.Count), this.Lparen.Line, this.Lparen.Column);
            }

            //类型判断
            for (int i = 0; i < argumentCount; i++)
            {
                var argumentType = argumentTypes[i].Type;
                var paramType = paramList[i].Type;

                //函数类返回值确定
                if (paramType.Name == Type.Function.Name && paramType.PackageName == Type.Function.PackageName &&
                    argumentType.Name == Type.Function.Name && argumentType.PackageName == Type.Function.PackageName)
                {
                    var pGenericityType = paramType as GenericityType;
                    var aGenericityType = argumentType as GenericityType;

                    if (pGenericityType.FirstGroupGenericTypeArguments == null)
                    {
                        pGenericityType.FirstGroupGenericTypeArguments = aGenericityType.FirstGroupGenericTypeArguments;
                    }
                }

                //类型匹配
                if (!argumentType.IsAssignableFrom(paramType))
                {
                    throw new SyntaxException(string.Format("Argument {0}: cannot convert from '{1}' to '{2}'.", i + 1, paramType.FullName, argumentType.FullName), Lparen.Line, Lparen.Column);
                }
            }
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append(string.Format("{0}.new(",this.Identifier.Symbol));
            Argumentlist.Symbol.GenerateLua(c, root, builder, depth);
            builder.Append(")");
        }
    }
}
        