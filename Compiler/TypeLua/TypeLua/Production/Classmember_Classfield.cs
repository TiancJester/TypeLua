
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <class member> ::= <class field>
    public class Classmember_Classfield : Class_member_basisproduction
    {
        public Token<Class_field_basisproduction> Classfield;

        public Classmember_Classfield(Project project, Class @class, GOLD.Token token0)
        {
            this.Classfield = new Token<Class_field_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Class_field_basisproduction)token0.Data};
            this.Children.Add(this.Classfield);
        }
    }
}
        