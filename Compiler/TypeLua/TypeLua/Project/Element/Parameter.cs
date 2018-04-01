// ----------------------------------------------------------------------------
// <author>HuHuiBin</author>
// <date>08/02/2018</date>
// ----------------------------------------------------------------------------
namespace TypeLua.Project.Element
{
    using TypeLua.Project.Types;

    public class Parameter : Variable
    {
        /// <summary>
        /// 复数数量参数（不定参数）
        /// </summary>
        public bool IsPlural;

        public Parameter(string name, Type type,bool isPlural)
            : base(name, type)
        {
            this.IsPlural = isPlural;
        }

        public override ContextElementCategory ElementCategory { get { return ContextElementCategory.Parameter; } }
    }
}