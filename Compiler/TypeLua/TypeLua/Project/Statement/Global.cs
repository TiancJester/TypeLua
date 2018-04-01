// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>03/03/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project.Statement
{
    using TypeLua.Project.Types;

    public class Global : Context
    {
        public new IContext ParentContext { get
        {
            return null;
        } }

        public new Class ClassContext { get
        {
            return null;
        } }
    }
}