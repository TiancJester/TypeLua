
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <class member> ::= <class function>
    public class Classmember_Classfunction : Class_member_basisproduction
    {
        public Token<Class_function_basisproduction> Classfunction;

        public Classmember_Classfunction(Project project, Class @class, GOLD.Token token0)
        {
            this.Classfunction = new Token<Class_function_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Class_function_basisproduction)token0.Data};
            this.Children.Add(this.Classfunction);
        }
    }
}
        