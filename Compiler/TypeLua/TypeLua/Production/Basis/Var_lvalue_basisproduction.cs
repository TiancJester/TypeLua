
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <var lvalue>
    public class Var_lvalue_basisproduction : Production
    {
        public virtual bool IsNeedRightValue()
        {
            return false;
        }

        public virtual Type GetLValueType(IContext context)
        {
            return null;
        }

        public virtual bool IsVariableDeclarer
        {
            get
            {
                return false;
            }
        }
    }
}
        