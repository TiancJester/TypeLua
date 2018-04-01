
namespace TypeLua.Production
{
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Types;

    // <emptyable type> ::= <type list>
    public class Emptyabletype_Typelist : Emptyable_type_basisproduction
    {
        public Token<Type_list_basisproduction> Typelist;

        public Emptyabletype_Typelist(Project project, Class @class, GOLD.Token token0)
        {
            this.Typelist = new Token<Type_list_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Type_list_basisproduction)token0.Data};
            this.Children.Add(this.Typelist);
        }

        public override Type[] GetTLType(PackagesContext packagesContext)
        {
            var tokens = this.Typelist.Symbol.GetTypes(new List<Token<Type_basisproduction>>());
            Type[] types = new Type[tokens.Count];
            for (int i = 0; i < tokens.Count; i++)
            {
                Token<Type_basisproduction> token = tokens[i];
                types[i] = token.Symbol.GetTLType(packagesContext);
            }
            return types;
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            this.Typelist.Symbol.GenerateLua(c, root, builder, depth + 1);
        }
    }
}
        