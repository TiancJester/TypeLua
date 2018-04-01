
namespace TypeLua.Production
{
    using TypeLua.GOLDBuilder;
    using TypeLua.Project;
    using TypeLua.Project.Types;

    // <class member> ::= <class ctor>
    public class Classmember_Classctor : Class_member_basisproduction
    {
        public Token<Class_ctor_basisproduction> Classctor;

        public Classmember_Classctor(Project project, Class @class, GOLD.Token token0)
        {
            this.Classctor = new Token<Class_ctor_basisproduction>() {Column = token0.Position().Column,Line = token0 .Position().Line,Symbol = (Class_ctor_basisproduction)token0.Data};
            this.Children.Add(this.Classctor);
        }

        public bool IsStaticCtor()
        {
            return this.Classctor.Symbol.IsStaticCtor();
        }
    }
}
        