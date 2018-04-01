// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>04/02/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project
{
    public interface IContext
    {
        IContext ParentContext { get; }

        Types.Class ClassContext { get; }

        ContextElement GetElement(string name, IContext current);

        void AddElement(ContextElement element);
    }

    public static class IContextHelper
    {
        public static ContextElement GetElementInParent(this IContext context, string name, IContext current)
        {
            do
            {
                var contextElement = context.GetElement(name, current);
                if (contextElement == null && context.ParentContext != null)
                {
                    context = context.ParentContext;
                }
                else
                {
                    return contextElement;
                }
            }
            while (true);
        }

        public static T1 GetParentContextWithUntil<T1,T2>(this IContext context) where T1 : class, IContext
        {
            IContext c = context;
            while (c != null)
            {
                if (c is T1)
                {
                    return c as T1;
                }
                if (c is T2)
                {
                    return null;
                }
                c = c.ParentContext;
            }
            return null;
        }

        public static T GetParentContextWith<T>(this IContext context) where T : class, IContext
        {
            IContext c = context;
            while (c != null)
            {
                if (c is T)
                {
                    return c as T;
                }
                c = c.ParentContext;
            }
            return null;
        }
    }
}