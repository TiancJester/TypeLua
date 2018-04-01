
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <type list> ::= <type list> ',' <type>
    public class Typelist_Typelist_Comma_Type : Type_list_basisproduction
    {
        public Token<Type_list_basisproduction> Typelist;
        public Token<string> Comma;
        public Token<Type_basisproduction> Type;

        public Typelist_Typelist_Comma_Type(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2)
        {
            this.Typelist = new Token<Type_list_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Type_list_basisproduction)token0.Data};
            this.Children.Add(this.Typelist);
            this.Comma = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Comma);
            this.Type = new Token<Type_basisproduction>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (Type_basisproduction)token2.Data};
            this.Children.Add(this.Type);
        }

        public override List<Token<Type_basisproduction>> GetTypes(List<Token<Type_basisproduction>> types)
        {
            if (types == null)
            {
                types = new List<Token<Type_basisproduction>>();
            }
            this.Typelist.Symbol.GetTypes(types);
            types.Add(this.Type);
            return types;
        }
    }
}
        