
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;

    // <else if statement list>
    public class Else_if_statement_list_basisproduction : Production
    {
        public virtual List<Token<Else_if_statement_basisproduction>> GetElseifs(List<Token<Else_if_statement_basisproduction>> elseifs)
        {
            return elseifs;
        }
    }
}
        