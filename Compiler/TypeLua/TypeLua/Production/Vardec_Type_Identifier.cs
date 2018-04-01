
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Element;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Types;

    // <var dec> ::= <type> Identifier
    public class Vardec_Type_Identifier : Var_dec_basisproduction
    {
        public Token<Type_basisproduction> Type;
        public Token<string> Identifier;

        public Vardec_Type_Identifier(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Type = new Token<Type_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Type_basisproduction)token0.Data};
            this.Children.Add(this.Type);
            this.Identifier = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Identifier);
        }

        public override void ContextVerify(IContext context)
        {
            var contextElement = context.GetElement(this.Identifier.Symbol, context);
            if (contextElement != null)
            {
                throw new SyntaxException("Variable with the same name is already exist", this.Identifier.Line, this.Identifier.Column);
            }
            var tlType = this.Type.Symbol.GetTLType(context.ClassContext.Packages);
            context.AddElement(new Variable(this.Identifier.Symbol, tlType));
        }

        public override Type GetVarType(IContext context)
        {
            var tlType = this.Type.Symbol.GetTLType(context.ClassContext.Packages);
            return tlType;
        }

        public override Token GetPositionToken(object param = null)
        {
            return Identifier;
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append(string.Format("local {0}", Identifier.Symbol));
        }
    }
}
        