
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;

    // <exp list>
    public class Exp_list_basisproduction : Production
    {
        public virtual List<Token<Exp_basisproduction>> GetExps(List<Token<Exp_basisproduction>> exps)
        {
            return exps;
        }
    }
}
        