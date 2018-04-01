
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Types;

    // <type> ::= HashTable '<' <type> ',' <type> '>'
    public class Type_Hashtable_Lt_Type_Comma_Type_Gt : Type_basisproduction
    {
        public Token<string> Hashtable;
        public Token<string> Lt;
        public Token<Type_basisproduction> Type;
        public Token<string> Comma;
        public Token<Type_basisproduction> Type_2;
        public Token<string> Gt;

        public Type_Hashtable_Lt_Type_Comma_Type_Gt(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2,GOLD.Token token3,GOLD.Token token4,GOLD.Token token5)
        {
            this.Hashtable = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Hashtable);
            this.Lt = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Lt);
            this.Type = new Token<Type_basisproduction>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (Type_basisproduction)token2.Data};
            this.Children.Add(this.Type);
            this.Comma = new Token<string>() {Column = token3.Position().Column,Line = token3 .Position().Line,Symbol = (string)token3.Data};
            this.Children.Add(this.Comma);
            this.Type_2 = new Token<Type_basisproduction>() { Column = token4.Position().Column,Line = token4 .Position().Line,Symbol = (Type_basisproduction)token4.Data };
            this.Children.Add(this.Type_2);
            this.Gt = new Token<string>() {Column = token5.Position().Column,Line = token5 .Position().Line,Symbol = (string)token5.Data};
            this.Children.Add(this.Gt);
        }

        public override Type GetTLType(PackagesContext packagesContext)
        {
            var types = new TypeLua.Project.Types.Type[] { this.Type.Symbol.GetTLType(packagesContext) , this.Type_2.Symbol.GetTLType(packagesContext) };
            
            return new GenericityType(this.Hashtable.Symbol, "", types, null,TypeCategory.Value);
        }
    }
}
        