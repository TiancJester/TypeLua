
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;

    // <statement list>
    public class Statement_list_basisproduction : Production
    {
        public virtual List<Token<Statement_basisproduction>> GetStatements(List<Token<Statement_basisproduction>> statements)
        {
            return statements;
        }
    }
}
        