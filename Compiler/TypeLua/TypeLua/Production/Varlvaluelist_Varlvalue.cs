
namespace TypeLua.Production
{
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <var lvalue list> ::= <var lvalue>
    public class Varlvaluelist_Varlvalue : Var_lvalue_list_basisproduction
    {
        public Token<Var_lvalue_basisproduction> Varlvalue;

        public Varlvaluelist_Varlvalue(Project project, Class @class, GOLD.Token token0)
        {
            this.Varlvalue = new Token<Var_lvalue_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Var_lvalue_basisproduction)token0.Data};
            this.Children.Add(this.Varlvalue);
        }

        public override List<Token<Var_lvalue_basisproduction>> GetLValues(List<Token<Var_lvalue_basisproduction>> lvalues)
        {
            if (lvalues == null)
            {
                lvalues = new List<Token<Var_lvalue_basisproduction>>();
            }
            lvalues.Add(this.Varlvalue);
            return lvalues;
        }

        public override Token GetPositionToken(object param = null)
        {
            return this.Varlvalue.Symbol.GetPositionToken(param);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            this.Varlvalue.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        