
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;

    // <type list>
    public class Type_list_basisproduction : Production
    {
        public virtual List<Token<Type_basisproduction>> GetTypes(List<Token<Type_basisproduction>> types)
        {
            return types;
        }
    }
}
        