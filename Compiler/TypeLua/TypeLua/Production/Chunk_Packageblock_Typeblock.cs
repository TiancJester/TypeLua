
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <chunk> ::= <package block> <type block>
    public class Chunk_Packageblock_Typeblock : Chunk_basisproduction
    {
        public Token<Package_block_basisproduction> Packageblock;
        public Token<Type_block_basisproduction> Typeblock;

        public Chunk_Packageblock_Typeblock(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Packageblock = new Token<Package_block_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Package_block_basisproduction)token0.Data};
            this.Children.Add(this.Packageblock);
            this.Typeblock = new Token<Type_block_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Type_block_basisproduction)token1.Data};
            this.Children.Add(this.Typeblock);
        }
    }
}
        