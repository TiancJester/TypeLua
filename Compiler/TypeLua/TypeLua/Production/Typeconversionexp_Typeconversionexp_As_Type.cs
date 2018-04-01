
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <type conversion exp> ::= <type conversion exp> as <type>
    public class Typeconversionexp_Typeconversionexp_As_Type : Type_conversion_exp_basisproduction
    {
        public Token<Type_conversion_exp_basisproduction> Typeconversionexp;
        public Token<string> As;
        public Token<Type_basisproduction> Type;

        public Typeconversionexp_Typeconversionexp_As_Type(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2)
        {
            this.Typeconversionexp = new Token<Type_conversion_exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Type_conversion_exp_basisproduction)token0.Data};
            this.Children.Add(this.Typeconversionexp);
            this.As = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.As);
            this.Type = new Token<Type_basisproduction>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (Type_basisproduction)token2.Data};
            this.Children.Add(this.Type);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            var tlType = this.Type.Symbol.GetTLType(packagesContext);
            if (!tlType.IsDecidedType())
            {
                var type = packagesContext.GetTLType(tlType.Name);
                tlType.PackageName = type.PackageName;
                tlType.Name = type.Name;
            }
            
            return this.GetExpressionsWithValue(tlType);
        }

        public override void ContextVerify(IContext context)
        {
            this.Typeconversionexp.Symbol.ContextVerify(context);
            this.Type.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Typeconversionexp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        