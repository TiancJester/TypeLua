
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;

    // <parameter list>
    public class Parameter_list_basisproduction : Production
    {
        public virtual List<Token<Parameter_basisproduction>> GetParams(List<Token<Parameter_basisproduction>> @params)
        {
            return @params;
        }
    }
}
        