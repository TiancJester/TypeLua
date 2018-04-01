
namespace TypeLua.Production
{
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <package name> ::= Identifier <package name list>
    public class Packagename_Identifier_Packagenamelist : Package_name_basisproduction
    {
        public Token<string> Identifier;
        public Token<Package_name_list_basisproduction> Packagenamelist;

        public Packagename_Identifier_Packagenamelist(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Identifier = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Identifier);
            this.Packagenamelist = new Token<Package_name_list_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Package_name_list_basisproduction)token1.Data};
            this.Children.Add(this.Packagenamelist);
        }

        public override string GetPackageName()
        {
            var packagenamelist = this.Packagenamelist.Symbol.GetNames(new List<Token<string>>());
            StringBuilder nameBuilder = new StringBuilder();
            nameBuilder.Append(this.Identifier.Symbol);
            foreach (var token in packagenamelist)
            {
                nameBuilder.Append(token.Symbol);
            }
            return nameBuilder.ToString();
        }
    }
}
        