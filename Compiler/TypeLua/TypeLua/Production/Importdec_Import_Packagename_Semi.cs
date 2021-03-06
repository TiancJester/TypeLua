
namespace TypeLua.Production
{
    using System;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Package;
    using TypeLua.Project.Types;

    // <import dec> ::= import <package name> ';'
    public class Importdec_Import_Packagename_Semi : Import_dec_basisproduction
    {
        public Token<string> Import;
        public Token<Package_name_basisproduction> Packagename;
        public Token<string> Semi;

        public Importdec_Import_Packagename_Semi(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2)
        {
            this.Import = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Import);
            this.Packagename = new Token<Package_name_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Package_name_basisproduction)token1.Data};
            this.Children.Add(this.Packagename);
            this.Semi = new Token<string>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (string)token2.Data};
            this.Children.Add(this.Semi);
        }

        public override bool BuildPackageContext(PackagesContext packages)
        {
            try
            {
                packages.Import(this.Packagename.Symbol.GetPackageName());
            }
            catch (Exception e)
            {
                throw new SyntaxException(e.Message, this.Import.Line, this.Import.Column);
            }
            return true;
        }
    }
}
        