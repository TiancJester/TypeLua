
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <class field assignment> ::= '=' <exp>
    public class Classfieldassignment_Eq_Exp : Class_field_assignment_basisproduction
    {
        public Token<string> Eq;
        public Token<Exp_basisproduction> Exp;

        public Classfieldassignment_Eq_Exp(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Eq = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Eq);
            this.Exp = new Token<Exp_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Exp_basisproduction)token1.Data};
            this.Children.Add(this.Exp);
        }

        public override void ContextVerify(IContext context)
        {
            this.Exp.Symbol.ContextVerify(context);
        }

        public override Expression GetExpression(IContext expContext)
        {
            var expType = this.Exp.Symbol.GetExpressions(expContext.ClassContext.Packages, expContext);
            if (expType != null || expType.Length > 0)
            {
                return expType[0];
            }
            return null;
        }

        public override Token GetPositionToken(object param = null)
        {
            return Eq;
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Exp.Symbol.GenerateLua(c,root,builder,depth);
        }
    }
}
        