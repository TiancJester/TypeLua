// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>12/02/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project.Element
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project.Types;

    public class Field : Variable, IClassMember
    {
        public Production DefineProduction;

        public Class ClassContext;

        public AccessType Access { get; set; }

        public Field(string name, AccessType access, Production defineProduction,Class classContext)
            : base(name)
        {
            this.DefineProduction = defineProduction;
            this.ClassContext = classContext;
            this.Access = access;
        }

        public override ContextElementCategory ElementCategory { get { return ContextElementCategory.Field; } }

    }
}