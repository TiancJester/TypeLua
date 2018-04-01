
namespace TypeLua.Production
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Element;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Generate;
    using TypeLua.Project.Package;
    using TypeLua.Project.Types;

    // <function body> ::= '(' <parameter list> ')' <block> end
    public class Functionbody_Lparen_Parameterlist_Rparen_Block_End : Function_body_basisproduction, IFunctionBody
    {
        public Token<string> Lparen;
        public Token<Parameter_list_basisproduction> Parameterlist;
        public Token<string> Rparen;
        public Token<Block_basisproduction> Block;
        public Token<string> End;

        private string appendCode;

        public Functionbody_Lparen_Parameterlist_Rparen_Block_End(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2,GOLD.Token token3,GOLD.Token token4)
        {
            this.Lparen = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Lparen);
            this.Parameterlist = new Token<Parameter_list_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Parameter_list_basisproduction)token1.Data};
            this.Children.Add(this.Parameterlist);
            this.Rparen = new Token<string>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (string)token2.Data};
            this.Children.Add(this.Rparen);
            this.Block = new Token<Block_basisproduction>() {Column = token3.Position().Column,Line = token3 .Position().Line,Symbol = (Block_basisproduction)token3.Data};
            this.Children.Add(this.Block);
            this.End = new Token<string>() {Column = token4.Position().Column,Line = token4 .Position().Line,Symbol = (string)token4.Data};
            this.Children.Add(this.End);

            this.SyntaxVerify();
        }

        public override bool SyntaxVerify()
        {
            var tlParameters = this.Parameterlist.Symbol.GetParams(new List<Token<Parameter_basisproduction>>());
            Dictionary<string, bool> bools = new Dictionary<string, bool>();
            foreach (var parameter in tlParameters)
            {
                var name = parameter.Symbol.GetParameter(null).Name;
                if (bools.ContainsKey(name))
                {
                    throw new SyntaxException("Parameter with the same name is already exist", parameter.Symbol.GetIdentifierToken().Line, parameter.Symbol.GetIdentifierToken().Column);
                }
                bools.Add(name, true);
            }
            return true;
        }

        public override Parameter[] GetParameters(PackagesContext packagesContext)
        {
            var @params = this.Parameterlist.Symbol.GetParams(new List<Token<Parameter_basisproduction>>());
            Parameter[] tlParameters = new Parameter[@params.Count];
            for (int i = 0; i < @params.Count; i++)
            {
                Token<Parameter_basisproduction> token = @params[i];
                tlParameters[i] = token.Symbol.GetParameter(packagesContext);
            }

            return tlParameters;
        }

        public override void ContextVerify(IContext context)
        {
            try
            {
                Function funcContext = null;
                if (context is Function)
                {
                    var func = context as Function;
                    if (func.Body == this)
                    {
                        funcContext = func;
                        this.SetFunctionContext(func);
                    }
                }
                if (funcContext == null)
                {
                    funcContext = this.GetFunctionContext(context);
                }
                this.Block.Symbol.ContextVerify(funcContext);
            }
            catch (MissingReturnException e)
            {
                throw new SyntaxException(e.Message,this.End.Line,this.End.Column);
            }
        }

        protected override Function OnGetFunctionContext(IContext context)
        {
            var function = new Function("", AccessType.Any, context.ClassContext, context, null, this.GetParameters(context.ClassContext.Packages), this);
            
            return function;
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            if (depth == 0)
            {
                //in class block
                builder.Append("(");
            }
            else
            {
                //in method block
                builder.Append("function(");
            }
            Parameterlist.Symbol.GenerateLua(c,root,builder,depth);
            builder.AppendLine(")");
            if (this.appendCode != null)
            {
                builder.Append(this.appendCode);
            }
            Block.Symbol.GenerateLua(c,root,builder,depth + 1);
            builder.Append(depth.GetIndentation());
            builder.Append("end");
        }

        public void AddFunctionCode(string code)
        {
            this.appendCode = code;
        }
    }
}
        