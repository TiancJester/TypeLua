
namespace TypeLua.Production
{
    using System.Collections.Generic;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <import list> ::= <import list> <import dec>
    public class Importlist_Importlist_Importdec : Import_list_basisproduction
    {
        public Token<Import_list_basisproduction> Importlist;
        public Token<Import_dec_basisproduction> Importdec;

        public Importlist_Importlist_Importdec(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Importlist = new Token<Import_list_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Import_list_basisproduction)token0.Data};
            this.Children.Add(this.Importlist);
            this.Importdec = new Token<Import_dec_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Import_dec_basisproduction)token1.Data};
            this.Children.Add(this.Importdec);
        }

        public override List<Token<Import_dec_basisproduction>> GetImportDecs(List<Token<Import_dec_basisproduction>> importdecs)
        {
            if (importdecs == null)
            {
                importdecs = new List<Token<Import_dec_basisproduction>>();
            }
            this.Importlist.Symbol.GetImportDecs(importdecs);
            importdecs.Add(this.Importdec);
            return importdecs;
        }
    }
}
        