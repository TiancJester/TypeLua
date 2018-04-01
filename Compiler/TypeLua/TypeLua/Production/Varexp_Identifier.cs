
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Element;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <var exp> ::= Identifier
    public class Varexp_Identifier : Var_exp_basisproduction
    {
        public Token<string> Identifier;

        public Varexp_Identifier(Project project, Class @class, GOLD.Token token0)
        {
            this.Identifier = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Identifier);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            var symbol = this.Identifier.Symbol;
            var t = packagesContext.GetTLType(symbol);
            if (t != null)
            {
                return this.GetExpressionsWithClass(t);
            }
            var contextElement = expContext.GetElementInParent(symbol, expContext);
            if (contextElement != null)
            {
                return this.GetExpressionsWithValue(contextElement.Type);
            }
            throw new SyntaxException(string.Format("Cannot resolve symbol '{0}'", this.Identifier.Symbol),this.Identifier.Line,this.Identifier.Column);
        }

        public override void ContextVerify(IContext context)
        {
            var tlValues = this.GetExpressions(context.ClassContext.Packages,context);
            if (tlValues == null || tlValues.Length == 0)
            {
                throw new SyntaxException(string.Format("Cannot resolve symbol '{0}'", this.Identifier.Symbol), this.Identifier.Line, this.Identifier.Column);
            }
        }

        public override Token GetPositionToken(object param = null)
        {
            return Identifier;
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            IClassMember classMember = c.GetElementInParent(this.Identifier.Symbol, c) as IClassMember;
            if (classMember != null)
            {
                if ((classMember.Access & AccessType.Global) > 0)
                {
                    builder.Append(this.Identifier.Symbol);
                }
                else if ((classMember.Access & AccessType.Static) > 0)
                {
                    builder.Append(string.Format("{0}.{1}", c.ClassName, this.Identifier.Symbol));
                }
                else
                {
                    builder.Append(string.Format("self.{0}", this.Identifier.Symbol));
                }
            }
            else
            {
                builder.Append(this.Identifier.Symbol);
            }
        }
    }
}
        