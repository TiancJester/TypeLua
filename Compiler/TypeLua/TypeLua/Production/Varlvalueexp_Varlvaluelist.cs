
namespace TypeLua.Production
{
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Types;

    // <var lvalue exp> ::= <var lvalue list>
    public class Varlvalueexp_Varlvaluelist : Var_lvalue_exp_basisproduction
    {
        public Token<Var_lvalue_list_basisproduction> Varlvaluelist;

        public Varlvalueexp_Varlvaluelist(Project project, Class @class, GOLD.Token token0)
        {
            this.Varlvaluelist = new Token<Var_lvalue_list_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Var_lvalue_list_basisproduction)token0.Data};
            this.Children.Add(this.Varlvaluelist);
        }

        public override void ContextVerify(IContext context)
        {
            var lValues = this.Varlvaluelist.Symbol.GetLValues(new List<Token<Var_lvalue_basisproduction>>());
            foreach (var lValue in lValues)
            {
                if (lValue.Symbol.IsNeedRightValue())
                {
                    throw new SyntaxException("The expressions lack right value.", lValue.Line, lValue.Column);
                }
                lValue.Symbol.ContextVerify(context);
            }
        }

        public override List<Token<Var_lvalue_basisproduction>> GetLValues()
        {
            return this.Varlvaluelist.Symbol.GetLValues(new List<Token<Var_lvalue_basisproduction>>());
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            this.Varlvaluelist.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        