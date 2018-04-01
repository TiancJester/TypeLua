
namespace TypeLua.Production
{
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <exp list> ::= <exp> ',' <exp list>
    public class Explist_Exp_Comma_Explist : Exp_list_basisproduction
    {
        public Token<Exp_basisproduction> Exp;
        public Token<string> Comma;
        public Token<Exp_list_basisproduction> Explist;

        public Explist_Exp_Comma_Explist(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2)
        {
            this.Exp = new Token<Exp_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Exp_basisproduction)token0.Data};
            this.Children.Add(this.Exp);
            this.Comma = new Token<string>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (string)token1.Data};
            this.Children.Add(this.Comma);
            this.Explist = new Token<Exp_list_basisproduction>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (Exp_list_basisproduction)token2.Data};
            this.Children.Add(this.Explist);
        }

        public override List<Token<Exp_basisproduction>> GetExps(List<Token<Exp_basisproduction>> exps)
        {
            if (exps == null)
            {
                exps = new List<Token<Exp_basisproduction>>();
            }
            exps.Add(this.Exp);
            this.Explist.Symbol.GetExps(exps);
            return exps;
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Exp.Symbol.GenerateLua(c,root,builder,depth);
            builder.Append(", ");
            Explist.Symbol.GenerateLua(c,root,builder,depth);
        }
    }
}
        