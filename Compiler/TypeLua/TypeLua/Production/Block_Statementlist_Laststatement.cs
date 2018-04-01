
namespace TypeLua.Production
{
    using System.Collections.Generic;
    using System.Text;

    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <block> ::= <statement list> <last statement>
    public class Block_Statementlist_Laststatement : Block_basisproduction
    {
        public Token<Statement_list_basisproduction> Statementlist;
        public Token<Last_statement_basisproduction> Laststatement;

        public Block_Statementlist_Laststatement(Project project, Class @class, GOLD.Token token0,GOLD.Token token1)
        {
            this.Statementlist = new Token<Statement_list_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Statement_list_basisproduction)token0.Data};
            this.Children.Add(this.Statementlist);
            this.Laststatement = new Token<Last_statement_basisproduction>() {Column = token1.Position().Column,Line = token1 .Position().Line,Symbol = (Last_statement_basisproduction)token1.Data};
            this.Children.Add(this.Laststatement);
        }

        public override void ContextVerify(IContext context)
        {
            var statements = this.Statementlist.Symbol.GetStatements(new List<Token<Statement_basisproduction>>());
            foreach (var statement in statements)
            {
                statement.Symbol.ContextVerify(context);
            }
            this.Laststatement.Symbol.ContextVerify(context);
        }

        public override void GenerateLua(Class c, string root, StringBuilder builder, int depth)
        {
            Statementlist.Symbol.GenerateLua(c, root, builder, depth);
            Laststatement.Symbol.GenerateLua(c, root, builder, depth);
        }
    }
}
        