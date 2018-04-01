
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;

    // <var lvalue list>
    public class Var_lvalue_list_basisproduction : Production
    {
        public virtual List<Token<Var_lvalue_basisproduction>> GetLValues(List<Token<Var_lvalue_basisproduction>> lvalues)
        {
            return lvalues;
        }
    }
}
        