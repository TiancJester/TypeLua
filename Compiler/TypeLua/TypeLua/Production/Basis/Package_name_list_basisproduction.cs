
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;

    // <package name list>
    public class Package_name_list_basisproduction : Production
    {
        public virtual List<Token<string>> GetNames(List<Token<string>> names)
        {
            return names;
        }
    }
}
        