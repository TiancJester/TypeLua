
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;

    // <var lvalue exp>
    public class Var_lvalue_exp_basisproduction : Production
    {
        public virtual List<Token<Var_lvalue_basisproduction>> GetLValues()
        {
            return null;
        }
    }
}
        