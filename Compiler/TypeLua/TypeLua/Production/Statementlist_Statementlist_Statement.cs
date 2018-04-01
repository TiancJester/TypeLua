
namespace TypeLua.Production
{
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <statement list> ::= <statement list> <statement>
    public class Statementlist_Statementlist_Statement : Statement_list_basisproduction
    {
        public Token<Statement_list_basisproduction> Statementlist;
        public Token<Statement_basisproduction> Statement;

        public Statementlist_Statementlist_Statement(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Statementlist = new Token<Statement_list_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Statement_list_basisproduction)token0.Data};
            this.Children.Add(this.Statementlist);
            this.Statement = new Token<Statement_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Statement_basisproduction)token1.Data};
            this.Children.Add(this.Statement);
        }

        public override List<Token<Statement_basisproduction>> GetStatements(List<Token<Statement_basisproduction>> statements)
        {
            if (statements == null)
            {
                statements = new List<Token<Statement_basisproduction>>();
            }
            this.Statementlist.Symbol.GetStatements(statements);
            statements.Add(this.Statement);
            return statements;
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            this.Statementlist.Symbol.GenerateLua(c,root,builder,depth);
            this.Statement.Symbol.GenerateLua(c,root,builder,depth);
        }
    }
}
        