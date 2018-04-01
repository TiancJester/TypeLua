
namespace TypeLua.Production
{
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <function call> ::= <object exp> '(' <argument list> ')'
    public class Functioncall_Objectexp_Lparen_Argumentlist_Rparen : Function_call_basisproduction
    {
        public Token<Object_exp_basisproduction> Objectexp;
        public Token<string> Lparen;
        public Token<Argument_list_basisproduction> Argumentlist;
        public Token<string> Rparen;

        public Functioncall_Objectexp_Lparen_Argumentlist_Rparen(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2,GOLD.Token token3)
        {
            this.Objectexp = new Token<Object_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Object_exp_basisproduction)token0.Data};
            this.Children.Add(this.Objectexp);
            this.Lparen = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Lparen);
            this.Argumentlist = new Token<Argument_list_basisproduction>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (Argument_list_basisproduction)token2.Data};
            this.Children.Add(this.Argumentlist);
            this.Rparen = new Token<string>() {Column = token3.Position().Column,Line = token3 .Position().Line,Symbol = (string)token3.Data};
            this.Children.Add(this.Rparen);
        }

        protected override Type[] OnGetReturnValues(PackagesContext packagesContext, IContext expContext)
        {
            GenericityType prefixType = this.GetPrefixType(packagesContext, expContext);
            return prefixType.FirstGroupGenericTypeArguments;
        }

        public override void ContextVerify(IContext context)
        {
            //形参
            GenericityType prefixType = this.GetPrefixType(context.ClassContext.Packages, context);
            var argumentTypes = prefixType.SecondGroupGenericTypeArguments;
            
            int argumentCount = 0;
            if (argumentTypes.Length == 0 || (argumentTypes.Length == 1 && argumentTypes[0] == Type.Void))
            {
                argumentCount = 0;
            }
            else
            {
                argumentCount = argumentTypes.Length;
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
            }

            //跳过Any类型的匹配检测
            if (prefixType == GenericityType.Any)
            {
                return;
            }
            
            //数量判断
            if (argumentCount != paramList.Count)
            {
                throw new SyntaxException(string.Format("The function has {0} parameter but is invoked with {1}", argumentCount, paramExpList.Count), this.Lparen.Line, this.Lparen.Column);
            }

            //类型判断
            for (int i = 0; i < argumentCount; i++)
            {
                var argumentType = argumentTypes[i];
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
                    throw new SyntaxException(string.Format("Argument {0}: cannot convert from '{1}' to '{2}'.",i + 1, paramType.FullName,argumentType.FullName), Lparen.Line, Lparen.Column);
                }
            }

            //参数上下文合法性检测
            for (int i = 0; i < paramExpList.Count; i++)
            {
                var paramExp = paramExpList[i];
                paramExp.Symbol.ContextVerify(context);
            }
        }

        private GenericityType GetPrefixType(PackagesContext packagesContext,IContext context)
        {
            var prefixList = this.Objectexp.Symbol.GetExpressions(packagesContext, context);
            if (prefixList.Length != 1)
            {
                throw new SyntaxException("Cannot call function here.", this.Lparen.Line, this.Lparen.Column);
            }
            var prefix = prefixList[0];

            if (prefix.Type == Type.Any || prefix.Type == Type.Table)
            {
                return GenericityType.Any;
            }

            if ( !(prefix.Type.Name == Type.Function.Name && prefix.Type.PackageName == Type.Function.PackageName))
            {
                throw new SyntaxException("Cannot call function here.", this.Lparen.Line, this.Lparen.Column);
            }
            var tlGenericityType = prefix.Type as GenericityType;
            if (tlGenericityType == null)
            {
                throw new SyntaxException("Cannot call function here.", this.Lparen.Line, this.Lparen.Column);
            }
            return tlGenericityType;
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Objectexp.Symbol.IsFunctionPrefix = true;
            Objectexp.Symbol.GenerateLua(c, root, builder, depth);
            builder.Append("(");
            Argumentlist.Symbol.GenerateLua(c, root, builder, depth);
            builder.Append(")");
        }
    }
}
        