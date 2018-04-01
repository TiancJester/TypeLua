// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>04/02/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project
{
    using TypeLua.Project.Element;

    public abstract class ContextElement
    {
        public string Name;

        public abstract ContextElementCategory ElementCategory { get; }

        public abstract Types.Type Type { get; set; }

        protected ContextElement(string name)
        {
            this.Name = name;
        }
    }
}