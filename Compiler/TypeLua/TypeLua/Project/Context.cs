// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>04/02/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project
{
    using System.Collections.Generic;

    public class Context : IContext
    {
        private Dictionary<string, ContextElement> elements = new Dictionary<string, ContextElement>();

        public IContext ParentContext { get; set; }

        private Types.Class classContext;

        public Types.Class ClassContext
        {
            get
            {
                if (this.classContext == null)
                {
                    this.classContext = this.GetParentContextWith<Types.Class>();
                }
                return this.classContext;
            }
            set
            {
                this.classContext = value;
            }
        }

        public Context()
        {
            
        }

        public ContextElement GetElement(string name, IContext current)
        {
            ContextElement element;
            this.elements.TryGetValue(name, out element);
            return element;
        }

        public void AddElement(ContextElement element)
        {
            this.elements.Add(element.Name, element);
        }

        public bool ContainsElement(string name)
        {
            return this.elements.ContainsKey(name);
        }

        public Dictionary<string, ContextElement> GetAllElements()
        {
            return this.elements;
        }
    }
}