
namespace TypeLua.Production
{
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <var lvalue list> ::= <var lvalue list> ',' <var lvalue>
    public class Varlvaluelist_Varlvaluelist_Comma_Varlvalue : Var_lvalue_list_basisproduction
    {
        public Token<Var_lvalue_list_basisproduction> Varlvaluelist;
        public Token<string> Comma;
        public Token<Var_lvalue_basisproduction> Varlvalue;

        public Varlvaluelist_Varlvaluelist_Comma_Varlvalue(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2)
        {
            this.Varlvaluelist = new Token<Var_lvalue_list_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Var_lvalue_list_basisproduction)token0.Data};
            this.Children.Add(this.Varlvaluelist);
            this.Comma = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Comma);
            this.Varlvalue = new Token<Var_lvalue_basisproduction>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (Var_lvalue_basisproduction)token2.Data};
            this.Children.Add(this.Varlvalue);
        }

        public override List<Token<Var_lvalue_basisproduction>> GetLValues(List<Token<Var_lvalue_basisproduction>> lvalues)
        {
            if (lvalues == null)
            {
                lvalues = new List<Token<Var_lvalue_basisproduction>>();
            }
            this.Varlvaluelist.Symbol.GetLValues(lvalues);
            lvalues.Add(this.Varlvalue);
            return lvalues;
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            this.Varlvaluelist.Symbol.GenerateLua(c, root, builder, depth);
            builder.Append(", ");
            this.Varlvalue.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        