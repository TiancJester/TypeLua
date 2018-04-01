
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Types;

    // <function call>
    public class Function_call_basisproduction : Production
    {
        private Type[] returnValues;
        public Type[] GetReturnValues(PackagesContext packagesContext, IContext expContext)
        {
            if (this.returnValues != null)
            {
                return this.returnValues;
            }
            this.returnValues = this.OnGetReturnValues(packagesContext, expContext);
            return this.returnValues;
        }

        protected virtual Type[] OnGetReturnValues(PackagesContext packagesContext, IContext expContext)
        {
            return null;
        }
    }
}
        