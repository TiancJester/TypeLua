
namespace TypeLua.Production
{
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <var lvalue exp> ::= <var lvalue list> '=' <exp list>
    public class Varlvalueexp_Varlvaluelist_Eq_Explist : Var_lvalue_exp_basisproduction
    {
        public Token<Var_lvalue_list_basisproduction> Varlvaluelist;
        public Token<string> Eq;
        public Token<Exp_list_basisproduction> Explist;

        public Varlvalueexp_Varlvaluelist_Eq_Explist(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2)
        {
            this.Varlvaluelist = new Token<Var_lvalue_list_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Var_lvalue_list_basisproduction)token0.Data};
            this.Children.Add(this.Varlvaluelist);
            this.Eq = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Eq);
            this.Explist = new Token<Exp_list_basisproduction>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (Exp_list_basisproduction)token2.Data};
            this.Children.Add(this.Explist);
        }

        public override List<Token<Var_lvalue_basisproduction>> GetLValues()
        {
            return this.Varlvaluelist.Symbol.GetLValues(new List<Token<Var_lvalue_basisproduction>>());
        }

        public override void ContextVerify(IContext context)
        {
            var lValues = this.Varlvaluelist.Symbol.GetLValues(new List<Token<Var_lvalue_basisproduction>>());
            foreach (var lValue in lValues)
            {
                lValue.Symbol.ContextVerify(context);
            }

            var rightExps = this.Explist.Symbol.GetExps(new List<Token<Exp_basisproduction>>());

            List<Expression> rValues = new List<Expression>();
            for (int i = 0; i < rightExps.Count; i++)
            {
                var rightExp = rightExps[i];
                var tlValues = rightExp.Symbol.GetExpressions(context.ClassContext.Packages, context);
                if (tlValues.Length == 0)
                {
                    throw new SyntaxException(
                        "Cannot use non-value expression here.",
                        this.Eq.Line,
                        this.Eq.Column);
                }
                rValues.AddRange(tlValues);
            }
            if (rValues.Count != lValues.Count)
            {
                throw new SyntaxException(
                        "The left value not match right value.",
                        this.Eq.Line,
                        this.Eq.Column);
            }
            for (int i = 0; i < rValues.Count; i++)
            {
                var rValue = rValues[i].Type;
                var lValue = lValues[i].Symbol.GetLValueType(context);

                if (lValue == null)
                {
                    throw new SyntaxException(
                        "The left value not match right value.",
                        this.Eq.Line,
                        this.Eq.Column);
                }
                if (rValue.Name == Type.Function.Name && rValue.PackageName == Type.Function.PackageName &&
                    lValue.Name == Type.Function.Name && lValue.PackageName == Type.Function.PackageName)
                {
                    var rGenericityType = rValue as GenericityType;
                    var lGenericityType = lValue as GenericityType;

                    if (rGenericityType.FirstGroupGenericTypeArguments == null)
                    {
                        rGenericityType.FirstGroupGenericTypeArguments = lGenericityType.FirstGroupGenericTypeArguments;
                    }
                }

                if (!lValue.IsAssignableFrom(rValue))
                {
                    throw new SyntaxException(
                        "The left value not match right value.",
                        this.Eq.Line,
                        this.Eq.Column);
                }
            }

            foreach (var rightExp in rightExps)
            {
                rightExp.Symbol.ContextVerify(context);
            }
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            this.Varlvaluelist.Symbol.GenerateLua(c, root, builder, depth);
            builder.Append(" = ");
            this.Explist.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        