// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>04/02/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project.Element
{
    using TypeLua.Project.Types;

    public class Variable : ContextElement
    {
        public override Type Type { get; set; }

        

        public Variable(string name)
            : base(name)
        {
        }

        public Variable(string name,Type type)
            : base(name)
        {
            this.Type = type;
        }

        public override ContextElementCategory ElementCategory { get { return ContextElementCategory.Variable; } }
    }
}