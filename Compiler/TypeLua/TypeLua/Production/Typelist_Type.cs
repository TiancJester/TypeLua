
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <type list> ::= <type>
    public class Typelist_Type : Type_list_basisproduction
    {
        public Token<Type_basisproduction> Type;

        public Typelist_Type(Project project, Class @class, GOLD.Token token0)
        {
            this.Type = new Token<Type_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Type_basisproduction)token0.Data};
            this.Children.Add(this.Type);
        }

        public override List<Token<Type_basisproduction>> GetTypes(List<Token<Type_basisproduction>> types)
        {
            if (types == null)
            {
                types = new List<Token<Type_basisproduction>>();
            }
            types.Add(this.Type);
            return types;
        }
    }
}
        