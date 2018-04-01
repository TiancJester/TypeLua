
namespace TypeLua.Production
{
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <argument list> ::= <exp list>
    public class Argumentlist_Explist : Argument_list_basisproduction
    {
        public Token<Exp_list_basisproduction> Explist;

        public Argumentlist_Explist(Project project, Class @class, GOLD.Token token0)
        {
            this.Explist = new Token<Exp_list_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Exp_list_basisproduction)token0.Data};
            this.Children.Add(this.Explist);
        }

        public override List<Token<Exp_basisproduction>> GetArguments(List<Token<Exp_basisproduction>> exps)
        {
            return this.Explist.Symbol.GetExps(exps);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Explist.Symbol.GenerateLua(c,root,builder,depth);
        }
    }
}
        