
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Types;

    // <var lvalue> ::= <var exp>
    public class Varlvalue_Varexp : Var_lvalue_basisproduction
    {
        public Token<Var_exp_basisproduction> Varexp;

        public Varlvalue_Varexp(Project project, Class @class, GOLD.Token token0)
        {
            this.Varexp = new Token<Var_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Var_exp_basisproduction)token0.Data};
            this.Children.Add(this.Varexp);
        }

        public override bool IsNeedRightValue()
        {
            return true;
        }

        public override void ContextVerify(IContext context)
        {
            this.Varexp.Symbol.ContextVerify(context);
        }

        public override Token GetPositionToken(object param = null)
        {
            return Varexp.Symbol.GetPositionToken(param);
        }

        public override Type GetLValueType(IContext context)
        {
            var tlValues = this.Varexp.Symbol.GetExpressions(context.ClassContext.Packages, context);
            if (tlValues.Length > 1)
            {
                throw new SyntaxException("Left value:Cannot use multi-value expression here.", this.Varexp.Line, this.Varexp.Column);
            }
            if (tlValues.Length == 0)
            {
                throw new SyntaxException("Left value:Cannot use non-value expression here.", this.Varexp.Line, this.Varexp.Column);
            }
            return tlValues[0].Type;
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            this.Varexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        