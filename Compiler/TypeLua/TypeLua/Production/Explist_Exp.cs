
namespace TypeLua.Production
{
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <exp list> ::= <exp>
    public class Explist_Exp : Exp_list_basisproduction
    {
        public Token<Exp_basisproduction> Exp;

        public Explist_Exp(Project project, Class @class, GOLD.Token token0)
        {
            this.Exp = new Token<Exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Exp_basisproduction)token0.Data};
            this.Children.Add(this.Exp);
        }

        public override List<Token<Exp_basisproduction>> GetExps(List<Token<Exp_basisproduction>> exps)
        {
            exps.Add(this.Exp);
            return exps;
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Exp.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        