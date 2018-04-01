// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>03/02/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.GOLDBuilder
{
    using System.Collections.Generic;
    using System.Text;

    using GOLD;

    using TypeLua.Production;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Types;

    public class Production
    {
        public List<Token> Children = new List<Token>();

        public virtual Token GetPositionToken(object param = null)
        {
            return null;
        }

        public virtual bool BuildClass(Project project, Class @class)
        {
            return true;
        }

        public virtual bool BuildPackageContext(PackagesContext packages)
        {
            return true;
        }

        public virtual void ContextVerify(IContext context)
        {
        }

        public virtual bool SyntaxVerify()
        {
            return true;
        }

        public virtual void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            
        }

    }
}