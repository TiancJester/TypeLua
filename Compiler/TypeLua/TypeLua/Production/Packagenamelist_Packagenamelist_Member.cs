
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <package name list> ::= <package name list> Member
    public class Packagenamelist_Packagenamelist_Member : Package_name_list_basisproduction
    {
        public Token<Package_name_list_basisproduction> Packagenamelist;
        public Token<string> Member;

        public Packagenamelist_Packagenamelist_Member(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Packagenamelist = new Token<Package_name_list_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Package_name_list_basisproduction)token0.Data};
            this.Children.Add(this.Packagenamelist);
            this.Member = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Member);
        }

        public override List<Token<string>> GetNames(List<Token<string>> names)
        {
            if (names == null)
            {
                names = new List<Token<string>>();
            }
            this.Packagenamelist.Symbol.GetNames(names);
            names.Add(this.Member);
            return names;
        }
    }
}
        