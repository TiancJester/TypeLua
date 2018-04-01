
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <package dec> ::= package <package name> ';'
    public class Packagedec_Package_Packagename_Semi : Package_dec_basisproduction
    {
        public Token<string> Package;
        public Token<Package_name_basisproduction> Packagename;
        public Token<string> Semi;

        public Packagedec_Package_Packagename_Semi(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2)
        {
            this.Package = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Package);
            this.Packagename = new Token<Package_name_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Package_name_basisproduction)token1.Data};
            this.Children.Add(this.Packagename);
            this.Semi = new Token<string>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (string)token2.Data};
            this.Children.Add(this.Semi);

            this.BuildClass(project, @class);
        }

        public override bool BuildClass(Project project, Class @class)
        {
            @class.PackageName = this.Packagename.Symbol.GetPackageName();
            return true;
        }
    }
}
        