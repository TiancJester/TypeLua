
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Element;
    using TypeLua.Project.Package;

    // <function body>
    public class Function_body_basisproduction : Production
    {
        private Function functionContext;

        public virtual Parameter[] GetParameters(PackagesContext packagesContext)
        {
            return null;
        }

        
        public Function GetFunctionContext(IContext context)
        {
            if (this.functionContext == null)
            {
                this.functionContext = this.OnGetFunctionContext(context);
            }
            return this.functionContext;
        }

        protected virtual Function OnGetFunctionContext(IContext context)
        {
            return null;
        }

        protected void SetFunctionContext(Function context)
        {
            this.functionContext = context;
        }
    }
}
        