
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project.Element;
    using TypeLua.Project.Package;

    // <parameter>
    public class Parameter_basisproduction : Production
    {
        public virtual Parameter GetParameter(PackagesContext packagesContext)
        {
            return null;
        }

        public virtual Token<string> GetIdentifierToken()
        {
            return null;
        }
    }
}
        