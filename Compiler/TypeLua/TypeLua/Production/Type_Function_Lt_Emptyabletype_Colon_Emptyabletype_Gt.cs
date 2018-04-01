
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Types;

    // <type> ::= function '<' <emptyable type> ':' <emptyable type> '>'
    public class Type_Function_Lt_Emptyabletype_Colon_Emptyabletype_Gt : Type_basisproduction
    {
        public Token<string> Function;
        public Token<string> Lt;
        public Token<Emptyable_type_basisproduction> Emptyabletype;
        public Token<string> Colon;
        public Token<Emptyable_type_basisproduction> Emptyabletype_2;
        public Token<string> Gt;

        public Type_Function_Lt_Emptyabletype_Colon_Emptyabletype_Gt(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2,GOLD.Token token3,GOLD.Token token4,GOLD.Token token5)
        {
            this.Function = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Function);
            this.Lt = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Lt);
            this.Emptyabletype = new Token<Emptyable_type_basisproduction>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (Emptyable_type_basisproduction)token2.Data};
            this.Children.Add(this.Emptyabletype);
            this.Colon = new Token<string>() {Column = token3.Position().Column,Line = token3 .Position().Line,Symbol = (string)token3.Data};
            this.Children.Add(this.Colon);
            this.Emptyabletype_2 = new Token<Emptyable_type_basisproduction>() {Column = token4.Position().Column,Line = token4 .Position().Line,Symbol = (Emptyable_type_basisproduction)token4.Data};
            this.Children.Add(this.Emptyabletype_2);
            this.Gt = new Token<string>() {Column = token5.Position().Column,Line = token5 .Position().Line,Symbol = (string)token5.Data};
            this.Children.Add(this.Gt);
        }

        public override Type GetTLType(PackagesContext packagesContext)
        {
            return new GenericityType(this.Function.Symbol,"",this.Emptyabletype.Symbol.GetTLType(packagesContext),this.Emptyabletype_2.Symbol.GetTLType(packagesContext),TypeCategory.Function);
        }
    }
}
        