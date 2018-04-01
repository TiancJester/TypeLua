
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <type block> ::= <class block>
    public class Typeblock_Classblock : Type_block_basisproduction
    {
        public Token<Class_block_basisproduction> Classblock;

        public Typeblock_Classblock(Project project, Class @class, GOLD.Token token0)
        {
            this.Classblock = new Token<Class_block_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Class_block_basisproduction)token0.Data};
            this.Children.Add(this.Classblock);
        }
    }
}
        