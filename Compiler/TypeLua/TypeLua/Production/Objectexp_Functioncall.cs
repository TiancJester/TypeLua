
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <object exp> ::= <function call>
    public class Objectexp_Functioncall : Object_exp_basisproduction
    {
        public Token<Function_call_basisproduction> Functioncall;

        public Objectexp_Functioncall(Project project, Class @class, GOLD.Token token0)
        {
            this.Functioncall = new Token<Function_call_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Function_call_basisproduction)token0.Data};
            this.Children.Add(this.Functioncall);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            var returnValues = this.Functioncall.Symbol.GetReturnValues(packagesContext, expContext);
            var tlValues = new Expression[returnValues.Length];
            for (int i = 0; i < returnValues.Length; i++)
            {
                var returnValue = returnValues[i];
                tlValues[i] = new Expression() {Classify = ExpressionType.Value,Type = returnValue};
            }
            return this.GetExpressions(tlValues);
        }

        public override void ContextVerify(IContext context)
        {
            this.Functioncall.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Functioncall.Symbol.GenerateLua(c,root,builder,depth);
        }
    }
}
        