
namespace TypeLua.Production
{
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <else if statement list> ::= <else if statement list> <else if statement>
    public class Elseifstatementlist_Elseifstatementlist_Elseifstatement : Else_if_statement_list_basisproduction
    {
        public Token<Else_if_statement_list_basisproduction> Elseifstatementlist;
        public Token<Else_if_statement_basisproduction> Elseifstatement;

        public Elseifstatementlist_Elseifstatementlist_Elseifstatement(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Elseifstatementlist = new Token<Else_if_statement_list_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Else_if_statement_list_basisproduction)token0.Data};
            this.Children.Add(this.Elseifstatementlist);
            this.Elseifstatement = new Token<Else_if_statement_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Else_if_statement_basisproduction)token1.Data};
            this.Children.Add(this.Elseifstatement);
        }

        public override List<Token<Else_if_statement_basisproduction>> GetElseifs(List<Token<Else_if_statement_basisproduction>> elseifs)
        {
            if (elseifs == null)
            {
                elseifs = new List<Token<Else_if_statement_basisproduction>>();
            }
            this.Elseifstatementlist.Symbol.GetElseifs(elseifs);
            elseifs.Add(this.Elseifstatement);
            return elseifs;
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            this.Elseifstatementlist.Symbol.GenerateLua(c, root, builder, depth);
            this.Elseifstatement.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        