
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Types;

    // <type> ::= ListTable '<' <type> '>'
    public class Type_Listtable_Lt_Type_Gt : Type_basisproduction
    {
        public Token<string> Listtable;
        public Token<string> Lt;
        public Token<Type_basisproduction> Type;
        public Token<string> Gt;

        public Type_Listtable_Lt_Type_Gt(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2,GOLD.Token token3)
        {
            this.Listtable = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Listtable);
            this.Lt = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Lt);
            this.Type = new Token<Type_basisproduction>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (Type_basisproduction)token2.Data};
            this.Children.Add(this.Type);
            this.Gt = new Token<string>() {Column = token3.Position().Column,Line = token3 .Position().Line,Symbol = (string)token3.Data};
            this.Children.Add(this.Gt);
        }

        public override Type GetTLType(PackagesContext packagesContext)
        {
            return new GenericityType(this.Listtable.Symbol, "", this.Type.Symbol.GetTLType(packagesContext), null,TypeCategory.Value);
        }
    }
}
        