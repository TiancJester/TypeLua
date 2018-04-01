
namespace TypeLua.Production
{
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Package;
    using TypeLua.Project.Statement;
    using TypeLua.Project.Types;

    // <object exp> ::= '(' <exp> ')'
    public class Objectexp_Lparen_Exp_Rparen : Object_exp_basisproduction
    {
        public Token<string> Lparen;
        public Token<Exp_basisproduction> Exp;
        public Token<string> Rparen;

        public Objectexp_Lparen_Exp_Rparen(Project project, Class @class, GOLD.Token token0,GOLD.Token token1,GOLD.Token token2)
        {
            this.Lparen = new Token<string>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (string)token0.Data};
            this.Children.Add(this.Lparen);
            this.Exp = new Token<Exp_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Exp_basisproduction)token1.Data};
            this.Children.Add(this.Exp);
            this.Rparen = new Token<string>() {Column = token2.Position().Column,Line = token2 .Position().Line,Symbol = (string)token2.Data};
            this.Children.Add(this.Rparen);
        }

        protected override Expression[] OnGetExpressions(PackagesContext packagesContext,IContext expContext)
        {
            return this.Exp.Symbol.GetExpressions(packagesContext,expContext);
        }

        public override void ContextVerify(IContext context)
        {
            this.Exp.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            builder.Append("(");
            Exp.Symbol.GenerateLua(c, root, builder, depth);
            builder.Append(")");
        }
    }
}
        