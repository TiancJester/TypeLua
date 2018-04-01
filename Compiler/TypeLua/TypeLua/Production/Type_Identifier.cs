
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Types;

    // <type> ::= Identifier
    public class Type_Identifier : Type_basisproduction
    {
        public Token<string> Identifier;

        public Type_Identifier(Project project, Class @class, GOLD.Token token0)
        {
            this.Identifier = new Token<string>() { Column = token0.Position().Column, Line = token0.Position().Line, Symbol = (string)token0.Data };
            this.Children.Add(this.Identifier);
        }

        public override Type GetTLType(PackagesContext packagesContext)
        {
            if (packagesContext == null)
            {
                return new Type(this.Identifier.Symbol, null, TypeCategory.Class);
            }
            return packagesContext.GetTLType(this.Identifier.Symbol);
        }
    }
}
        