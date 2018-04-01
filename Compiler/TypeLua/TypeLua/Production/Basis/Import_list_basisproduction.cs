
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;

    // <import list>
    public class Import_list_basisproduction : Production
    {
        public virtual List<Token<Import_dec_basisproduction>> GetImportDecs(List<Token<Import_dec_basisproduction>> importdecs)
        {
            return importdecs;
        }
    }
}
        