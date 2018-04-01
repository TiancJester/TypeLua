// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>03/03/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project.Statement
{
    using TypeLua.Project.Types;

    public class Expression
    {
        public ExpressionType Classify;

        public Type Type;

        public ContextElement Element;
    }

    public enum ExpressionType
    {
        Value,
        Class,
        This,
        Super,
    }
}