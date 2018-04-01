
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <class member> ::= <class extern function>
    public class Classmember_Classexternfunction : Class_member_basisproduction
    {
        public Token<Class_extern_function_basisproduction> Classexternfunction;

        public Classmember_Classexternfunction(Project project, Class @class, GOLD.Token token0)
        {
            this.Classexternfunction = new Token<Class_extern_function_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Class_extern_function_basisproduction)token0.Data};
            this.Children.Add(this.Classexternfunction);
        }
    }
}
        