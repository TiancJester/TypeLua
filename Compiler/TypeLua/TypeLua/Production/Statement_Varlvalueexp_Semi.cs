
namespace TypeLua.Production
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Generate;
    using TypeLua.Project.Types;

    // <statement> ::= <var lvalue exp> ';'
    public class Statement_Varlvalueexp_Semi : Statement_basisproduction
    {
        public Token<Var_lvalue_exp_basisproduction> Varlvalueexp;
        public Token<string> Semi;
        

        public Statement_Varlvalueexp_Semi(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Varlvalueexp = new Token<Var_lvalue_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Var_lvalue_exp_basisproduction)token0.Data};
            this.Children.Add(this.Varlvalueexp);
            this.Semi = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Semi);
        }

        public override void ContextVerify(IContext context)
        {
            var lValues = this.Varlvalueexp.Symbol.GetLValues();
            if (lValues.Count > 1 && lValues[0].Symbol.IsVariableDeclarer)
            {
                var positionToken = lValues[1].Symbol.GetPositionToken();
                throw new SyntaxException("There can only be one variable in the declaration.", positionToken.Line, positionToken.Column);
            }
            if (lValues.Count > 1)
            {
                for (int i = 1; i < lValues.Count; i++)
                {
                    var lValue = lValues[i];
                    if (lValue.Symbol.IsVariableDeclarer)
                    {
                        var positionToken = lValue.Symbol.GetPositionToken();
                        throw new SyntaxException("Cannot decare variable here.", positionToken.Line, positionToken.Column);
                    }
                }
            }
            this.Varlvalueexp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append(depth.GetIndentation());
            this.Varlvalueexp.Symbol.GenerateLua(c, root, builder, depth);
            builder.AppendLine();
        }
    }
}
        