
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;

    // <argument list>
    public class Argument_list_basisproduction : Production
    {
        public virtual List<Token<Exp_basisproduction>> GetArguments(List<Token<Exp_basisproduction>> exps)
        {
            return exps;
        }
    }
}
        