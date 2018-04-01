
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project.Package;
    using TypeLua.Project.Types;

    // <emptyable type>
    public class Emptyable_type_basisproduction : Production
    {
        public virtual bool IsEmptyType()
        {
            return false;
        }

        public virtual Type[] GetTLType(PackagesContext packagesContext)
        {
            return null;
        }
    }
}
        