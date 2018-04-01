
namespace TypeLua.Production
{
    using System;
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Exception;
    using TypeLua.Project.Package;
    using TypeLua.Project.Types;

    // <package block> ::= <package dec> <import list>
    public class Packageblock_Packagedec_Importlist : Package_block_basisproduction
    {
        public Token<Package_dec_basisproduction> Packagedec;
        public Token<Import_list_basisproduction> Importlist;

        public Packageblock_Packagedec_Importlist(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Packagedec = new Token<Package_dec_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Package_dec_basisproduction)token0.Data};
            this.Children.Add(this.Packagedec);
            this.Importlist = new Token<Import_list_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Import_list_basisproduction)token1.Data};
            this.Children.Add(this.Importlist);

            this.BuildClass(project, @class);
        }

        public override bool BuildClass(Project project, Class @class)
        {
            @class.Packages.PackageList = this;
            return true;
        }

        public override bool BuildPackageContext(PackagesContext packages)
        {
            try
            {
                packages.Import(packages.Class.PackageName);
            }
            catch (Exception e)
            {
                throw new SyntaxException(e.Message, this.Packagedec.Line, this.Packagedec.Column);
            }

            var importDecs = this.Importlist.Symbol.GetImportDecs(new List<Token<Import_dec_basisproduction>>());
            foreach (var importDec in importDecs)
            {
                importDec.Symbol.BuildPackageContext(packages);
            }
            return true;
        }
    }
}
        