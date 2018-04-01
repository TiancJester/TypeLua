
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <exp>
    public abstract class Exp_basisproduction : Production
    {
        protected Expression[] Expressions;

        public virtual Exp_basisproduction GetFinalExp()
        {
            return this;
        }

        
        public Expression[] GetExpressions(PackagesContext packagesContext, IContext expContext)
        {
            if (this.Expressions == null)
            {
                this.Expressions = this.OnGetExpressions(packagesContext, expContext);
            }
            return this.Expressions;
        }

        public bool ExpTypeIs(Type type, PackagesContext packagesContext, IContext expContext)
        {
            var tlValues = this.GetExpressions(packagesContext,expContext);
            if (tlValues.Length == 1)
            {
                return tlValues[0].Type == type;
            }
            return false;
        }

        protected abstract Expression[] OnGetExpressions(PackagesContext packagesContext, IContext expContext);

        protected Expression[] GetExpressions(params Expression[] value)
        {
            return value;
        }

        protected Expression[] GetExpressionsWithValue(Type value)
        {
            return new[] { new Expression() { Classify = ExpressionType.Value, Type = value } };
        }

        protected Expression[] GetExpressionsWithClass(Type value)
        {
            this.Expressions = new[] { new Expression() { Classify = ExpressionType.Class, Type = value } };
            return this.Expressions;
        }
    }
}
        